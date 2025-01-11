using Infrastructure.Users.DomainModels;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPIGateway.Services.Authentication.Models
{
    public class LoginOutputModel
    {
        public User? UserDetails { get; set; }
        public JwtModel? JwtModel { get; set; }
    }
}
