namespace Infrastructure.Users.DomainModels
{
    [Flags]
    public enum UserRole : int
    {
        Admin = 0,
        Customer = 1
    }
}
