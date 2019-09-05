using System;

namespace EShop.Catalog.Shared
{
    public class Product
    {
        public Product(string id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
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