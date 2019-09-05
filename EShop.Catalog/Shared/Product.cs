using System;

namespace EShop.Catalog.Shared
{
    public sealed class Product
    {
        public Product(string id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public Product(string name, double price)
            : this(Guid.NewGuid().ToString(), name, price)
        {
        }

        public string Id { get; }

        public string Name { get; }

        public double Price { get; }
    }
}