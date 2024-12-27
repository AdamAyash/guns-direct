namespace Infrastructure.Users.UsersTable
{
    using DatabaseCoreKit.Database.Table;
    using Infrastructure.Users.DomainModels;

    public class UsersTable : BaseTableTemplate<User>
    {
        public UsersTable()
            : base("USERS")
        {
        }
    }
}
