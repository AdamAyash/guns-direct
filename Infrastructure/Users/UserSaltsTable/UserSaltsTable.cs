using DatabaseCoreKit.Database.Table;
using Infrastructure.Users.DomainModels;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Users.UserSaltsTable
{
    public class UserSaltsTable : BaseTableTemplate<UserSalt>
    {
        public UserSaltsTable() 
            : base("USER_SALTS")
        {
        }

        public UserSaltsTable(SqlConnection sqlConnection)
            : base(sqlConnection, "USER_SALTS")
        {
        }
    }
}
