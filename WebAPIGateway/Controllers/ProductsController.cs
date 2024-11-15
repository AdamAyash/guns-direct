namespace WebAPIGateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello, world!";
        }

        [HttpPost]
        public ActionResult<string> UpdateProduct([FromBody] string name)
        {
            return "Success";
        }
    }
}
