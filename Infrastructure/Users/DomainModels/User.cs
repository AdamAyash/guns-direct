using DatabaseCoreKit;

namespace Infrastructure.Users.DomainModels
{
    public class User : DomainObject
    {
        public int ID { get; set; }
        public int UpdateCounter { get; set; }
        public int Role { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
