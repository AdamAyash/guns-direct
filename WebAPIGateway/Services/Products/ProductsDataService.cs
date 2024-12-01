using Infrastructure.Products.ProductsTable;
using WebAPIGateway.Services.Products.Models;

namespace WebAPIGateway.Services.Products
{
    public class ProductsDataService : IProductsDataService
    {
        private ProductsTable _productsTable;

        public ProductsDataService()
        {
            _productsTable = new ProductsTable();
        }

        public async Task<GetAllProductsOutputModel> GetAllProductsAsync(GetAllProductsInputModel inputModel)
        {
            var products = new GetAllProductsOutputModel();
            if(!_productsTable.SelectAll(products.Products))
            {
            }

            return products;
        }
       
    }
}
