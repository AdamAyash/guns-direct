using DatabaseCoreKit;

namespace Infrastructure.Users.DomainModels
{
    public class UserSalt : DomainObject
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }

        public string Salt { get; set; }

        public UserSalt()
        {
            
        }
    }
}
