using Infrastructure.Users.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using WebAPIGateway.Common;
using WebAPIGateway.Services.Authentication;
using WebAPIGateway.Services.Authentication.Models;
using WebAPIGateway.Services.Base;
using WebAPIGateway.Services.Products.Models;

namespace WebAPIGateway.Controllers
{
    [Route("/authentication")]
    [ApiController]
    public class AuthenticationController : SmartBaseController
    {
        private ILogger<ProductsController> _logger;
        private IJwtTService _jwtService;

        public AuthenticationController(ILogger<ProductsController> logger, IJwtTService jwtService)
        {
            this._logger = logger;
            this._jwtService = jwtService;
        }

        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(BaseServerResponse<IActionResult>))]
        public async Task<BaseServerResponse<LoginOutputModel>> LoginAsync([FromBody] LoginInputModel inputModel)
        {
            var result = new LoginOutputModel() {
                JwtToken = this._jwtService.GenerateSecurityToken(new User() {Email = "randommail@mail.com" })
            };

            return this.GenerateAPIResponse<LoginOutputModel>(true, result);
        }
    }
}
