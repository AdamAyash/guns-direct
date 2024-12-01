namespace WebAPIGateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Web.Http.Description;
    using WebAPIGateway.Services.Products;
    using WebAPIGateway.Services.Products.Models;

    [Route("/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ILogger<ProductsController> _logger;
        private IProductsDataService _productsDataService;

        public ProductsController(ILogger<ProductsController> logger, IProductsDataService productsDataService)
        {
            this._logger = logger;
            this._productsDataService = productsDataService;
        }

        [HttpPost]
        [Route("get_all_products")]
        [ResponseType(typeof(GetAllProductsOutputModel))]
        public async Task<GetAllProductsOutputModel> GetAllProductsAsync([FromBody] GetAllProductsInputModel inputModel)
        {
            return await _productsDataService.GetAllProductsAsync(inputModel);
        }
    }
}
