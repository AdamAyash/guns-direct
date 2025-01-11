namespace Infrastructure.Users.UsersTable
{
    using DatabaseCoreKit.Database.Table;
    using Infrastructure.Users.DomainModels;
    using Microsoft.Data.SqlClient;

    public class UsersTable : BaseTableTemplate<User>
    {
        public enum UsersTableColumns
        {
            Email,
            Password
        }

        public UsersTable()
            : base("USERS")
        {
        }

        public UsersTable(SqlConnection sqlConnection)
           : base(sqlConnection, "USERS")
        {
        }
    }
}
