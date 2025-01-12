using Infrastructure.Users.DomainModels;

namespace WebAPIGateway.Services.Authentication.Models
{
    public class RegisterOutputModel
    {
        public User UserDetails { get; set; }
    }
}
