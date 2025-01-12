using WebAPIGateway.Services.Authentication.Models;

namespace WebAPIGateway.Services.Authentication
{
    public interface IUserAuthenticationService
    {
        public Task<bool> GetUserAsync(LoginInputModel inputModel, LoginOutputModel outpuModel);
        public Task<bool> RegisterNewUser(RegisterInputModel inputModel);
    }
}
