﻿namespace Infrastructure.Products.DomainModels
{
    using DatabaseCoreKit;
    using Newtonsoft.Json;

    public class Product : DomainObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCategory { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
