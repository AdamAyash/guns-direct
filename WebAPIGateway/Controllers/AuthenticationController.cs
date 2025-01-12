using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Web.Http.Description;
using WebAPIGateway.Common;
using WebAPIGateway.Services.Authentication;
using WebAPIGateway.Services.Authentication.Models;
using WebAPIGateway.Services.Base;

namespace WebAPIGateway.Controllers
{
    [Route("/authentication")]
    [ApiController]
    public class AuthenticationController : SmartBaseController
    {
        private ILogger<ProductsController> _logger;
        private IJwtTService _jwtService;
        private IUserAuthenticationService _userAuthenticationService;
        private readonly int _jwtExpireTimeStampInSeconds = 900; // 15 minutes;

        public AuthenticationController(ILogger<ProductsController> logger,
            IJwtTService jwtService,
            IUserAuthenticationService userAuthenticationService,
            IMemoryCache memoryCache)
        {
            this._logger = logger;
            this._jwtService = jwtService;
            this._userAuthenticationService = userAuthenticationService;

        }

        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(BaseServerResponse<LoginOutputModel>))]
        public async Task<BaseServerResponse<LoginOutputModel>> LoginAsync([FromBody] LoginInputModel inputModel)
        {
            bool isSuccessfull = true;
            var loginOutputModel = new LoginOutputModel();

            isSuccessfull = await this._userAuthenticationService.GetUserAsync(inputModel, loginOutputModel);
            if (loginOutputModel.UserDetails != null)
            {
                var JwtPayload = this._jwtService.GenerateSecurityToken(loginOutputModel.UserDetails);
                loginOutputModel.JwtModel = new JwtModel();
                loginOutputModel.JwtModel.Payload = JwtPayload;
                loginOutputModel.JwtModel.UserId = loginOutputModel.UserDetails.ID.ToString();
                loginOutputModel.JwtModel.TokenType = "Bearer";
                loginOutputModel.JwtModel.ExpiresIn = this._jwtExpireTimeStampInSeconds;
            }

            return this.GenerateAPIResponse<LoginOutputModel>(isSuccessfull, loginOutputModel);
        }

        [HttpPost]
        [Route("register")]
        [ResponseType(typeof(BaseServerResponse<RegisterOutputModel>))]
        public async Task<BaseServerResponse<RegisterOutputModel>> RegisterAsync([FromBody] RegisterInputModel inputModel)
        {
            bool isSuccessfull = true;
            var registerOutputModel = new RegisterOutputModel();
            isSuccessfull = await this._userAuthenticationService.RegisterNewUser(inputModel);

            return this.GenerateAPIResponse<RegisterOutputModel>(isSuccessfull, registerOutputModel);
        }
    }
}
