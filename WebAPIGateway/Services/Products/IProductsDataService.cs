﻿
namespace WebAPIGateway.Services.Products
{
    using WebAPIGateway.Services.Products.Models;

    public interface IProductsDataService
    {
        public Task<GetAllProductsOutputModel> GetAllProductsAsync(GetAllProductsInputModel inputModel);

        public Task<GetProductByIdOutputModel> GetProductByIdAsync(GetProductByIdInputModel inputModel);
    }
}
