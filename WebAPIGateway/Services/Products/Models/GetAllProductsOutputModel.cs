using Infrastructure.Products.DomainModels;

namespace WebAPIGateway.Services.Products.Models
{
    public class GetAllProductsOutputModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
