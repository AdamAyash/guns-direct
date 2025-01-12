using Infrastructure.Products.DomainModels;
using Infrastructure.Products.ProductsTable;
using WebAPIGateway.Services.Products.Models;

namespace WebAPIGateway.Services.Products
{
    public class ProductsDataService : IProductsDataService
    {
        private ILogger<ProductsDataService> _logger;
        private readonly ProductsTable _productsTable;

        public ProductsDataService(ILogger<ProductsDataService> logger)
        {   
            this._logger = logger;
            this._productsTable = new ProductsTable();
        }

        public async Task<GetAllProductsOutputModel> GetAllProductsAsync(GetAllProductsInputModel inputModel)
        {
            var products = new GetAllProductsOutputModel();
            if(!_productsTable.SelectAll(products.Products))
            {
                //this._logger.Log(LogLevel.Error, "Test");
            }

            return await Task.FromResult(products);
        }

        public async Task<GetProductByIdOutputModel> GetProductByIdAsync(GetProductByIdInputModel inputModel)
        {
            var product = new GetProductByIdOutputModel();

            int productId = int.Parse(inputModel.ProductId);

            if (!_productsTable.SelectByPrimaryKey(productId, product.ProductData))
            {
            }

            return await Task.FromResult(product);
        }
    }
}
