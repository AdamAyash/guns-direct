using Infrastructure.Products.DomainModels;

namespace WebAPIGateway.Services.Products.Models
{
    public class GetProductByIdOutputModel
    {
        public Product ProductData { get; set; }

        public GetProductByIdOutputModel()
        {
            this.ProductData = new Product();
        }

    }
}
