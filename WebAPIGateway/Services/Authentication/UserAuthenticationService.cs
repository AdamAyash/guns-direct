using Common;
using DatabaseCoreKit;
using Infrastructure.Users.DomainModels;
using Infrastructure.Users.UserSaltsTable;
using Infrastructure.Users.UsersTable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using WebAPIGateway.Services.Authentication.Models;
using WebAPIGateway.Services.CryptographicService;

namespace WebAPIGateway.Services.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private const string USERS_CACHE_KEY = "users";

        private readonly Logger _logger = Logger.GetLoggerInstance();
        private readonly IMemoryCache _memoryCache;
        private readonly ICryptographicService _cryptographicService;

        public UserAuthenticationService(IMemoryCache memoryCache, ICryptographicService cryptographicService)
        {
            this._memoryCache = memoryCache;
            this._cryptographicService = cryptographicService;
        }

        public Task<bool> GetUserAsync(LoginInputModel inputModel, LoginOutputModel outpuModel)
        {
            var loginOutputModel = new LoginOutputModel();
            List<User>? users = this._memoryCache.Get<List<User>>(USERS_CACHE_KEY);

            UsersTable usersTable = new UsersTable();

            if(users == null)
            {
                users = new List<User>();
                if (!usersTable.SelectAll(users))
                {
                    this._logger.LogError("An error orccured while trying to retrieve user information.");
                    return Task.FromResult(false);
                }
                this._memoryCache.Set<List<User>>(USERS_CACHE_KEY, users);
            }

            UserSaltsTable userSaltsTable = new UserSaltsTable();
            SQLComplexKey oComplexLey = new SQLComplexKey("EMAIL", inputModel.Email);

            UserSalt userSalt = new UserSalt();
            if(!userSaltsTable.SelectByComplexKey(oComplexLey , ref userSalt)) 
            {
                this._logger.LogError("User does not exist.");
                return Task.FromResult(false);
            }

            if(userSalt.ID <= 0)
            {
                this._logger.LogError("User does not exist.");
                return Task.FromResult(false);
            }

            byte[] salt = Convert.FromBase64String(userSalt.Salt);
            string hashedPassword = _cryptographicService.HashPassword(inputModel.Password, salt);

            User? userDetails = users.Where(user => user.Password == hashedPassword && user.Email == inputModel.Email).FirstOrDefault();
            if (userDetails?.ID > 0)
            {
                outpuModel.UserDetails = userDetails;
            }
            else
            {
                this._logger.LogError("User does not exist.");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> RegisterNewUser(RegisterInputModel inputModel)
        {
            DatabaseConnectionPool databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();
            SqlConnection? sqlConnection = databaseConnectionPool.GetDatabaseConnection();

            UsersTable usersTable = new UsersTable(sqlConnection);

            SQLComplexKey getUserByEmailComplexKey = new SQLComplexKey(UsersTable.UsersTableColumns.Email.ToString(), inputModel.Email);
            User user = new User();

            if (!usersTable.SelectByComplexKey(getUserByEmailComplexKey, ref user))
            {
                this._logger.LogError("UserAuthenticationService error.");
                return Task.FromResult(false);
            }

            if(user.ID > 0)
            {
                this._logger.LogError("User already exists");
                return Task.FromResult(false);
            }

            byte[] salt = this._cryptographicService.GenerateSalt();
            string hashedPassword = this._cryptographicService.HashPassword(inputModel.ConfirmedPassword, salt);

            User newUser = new User();
            newUser.Password = hashedPassword;
            newUser.FirstName = inputModel.FirstName;
            newUser.LastName = inputModel.LastName;
            newUser.MiddleName = inputModel.MiddleName;
            newUser.Email = inputModel.Email;
            newUser.RegisterDate = DateTime.Now;
            newUser.DateOfBirth = inputModel.DateOfBirth;
            newUser.Phone = inputModel.Phone;
            newUser.Role = (int)UserRole.Customer;

            if (!usersTable.Insert(newUser))
            {
                if (!databaseConnectionPool.ReleaseConnection(sqlConnection))
                {
                    this._logger.LogError("UserAuthenticationService error.");
                    return Task.FromResult(false);
                }
                return Task.FromResult(false);
            }

            if (!usersTable.SelectByComplexKey(getUserByEmailComplexKey, ref user))
            {
                this._logger.LogError("UserAuthenticationService error.");
                return Task.FromResult(false);
            }

            UserSalt userSalt = new UserSalt();
            userSalt.Salt = Convert.ToBase64String(salt);
            userSalt.UserId = user.ID;
            userSalt.Email = user.Email;

            UserSaltsTable userSaltsTable = new UserSaltsTable(sqlConnection);
            userSaltsTable.SetTransactionContext(usersTable.GetTransactionContext());

            if (!userSaltsTable.Insert(userSalt))
            {
                this._logger.LogError("UserAuthenticationService error.");
                return Task.FromResult(false);
            }

            if(!databaseConnectionPool.ReleaseConnection(sqlConnection))
            {
                this._logger.LogError("UserAuthenticationService error.");
                return Task.FromResult(false);
            }

            if(!usersTable.CommitTransaction())
            {
                this._logger.LogError("UserAuthenticationService error.");
                return Task.FromResult(false);
            }

            List<User>? users = this._memoryCache.Get<List<User>>(USERS_CACHE_KEY);

            if(users != null)
            {
                users.Add(newUser);
                this._memoryCache.Set(USERS_CACHE_KEY, users);
            }

            return Task.FromResult(true);
        }

    }
}
