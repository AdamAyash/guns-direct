using Infrastructure.Users.DomainModels;

namespace WebAPIGateway.Services.Authentication
{
    public interface IJwtTService
    {
        public string GenerateSecurityToken(User userModel);
    }
}
