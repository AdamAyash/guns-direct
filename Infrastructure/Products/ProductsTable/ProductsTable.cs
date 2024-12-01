using DatabaseCoreKit.Database.Table;
using Infrastructure.Products.DomainModels;

namespace Infrastructure.Products.ProductsTable
{
    public class ProductsTable : BaseTableTemplate<Product>
    {
        public ProductsTable()
            : base("PRODUCTS")
        {
            
        }
    }
}
