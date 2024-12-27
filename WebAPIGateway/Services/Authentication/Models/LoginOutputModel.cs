using System.IdentityModel.Tokens.Jwt;

namespace WebAPIGateway.Services.Authentication.Models
{
    public class LoginOutputModel
    {
        public string JwtToken { get; set; }
    }
}
