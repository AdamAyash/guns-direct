namespace WebAPIGateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Web.Http.Description;
    using WebAPIGateway.Common;
    using WebAPIGateway.Services.Base;
    using WebAPIGateway.Services.Products;
    using WebAPIGateway.Services.Products.Models;

    [Route("/products")]
    [ApiController]
    public class ProductsController : SmartBaseController
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
        [ResponseType(typeof(BaseServerResponse<GetAllProductsOutputModel>))]
        public async Task<BaseServerResponse<GetAllProductsOutputModel>> GetAllProductsAsync([FromBody] GetAllProductsInputModel inputModel)
        {
            bool isSuccessfull = true;
            var getAllProductsTask = new GetAllProductsOutputModel();

            try
            {
               getAllProductsTask = await _productsDataService.GetAllProductsAsync(inputModel);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception.Message);
                isSuccessfull = false;
            }

            return this.GenerateAPIResponse<GetAllProductsOutputModel>(isSuccessfull, getAllProductsTask);
        }
    }
}
