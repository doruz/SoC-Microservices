using System.Collections.Generic;
using System.Linq;

namespace EShop.Catalog.Shared
{
    public sealed class ProductsRepository
    {
        private readonly List<Product> products;

        public ProductsRepository()
        {
            this.products = new List<Product>
            {
                new Product("Clean Code Book", 10.5),
                new Product("JBL GO Speaker", 100)
            };
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.products.OrderBy(p => p.Name).ToList();
        }

        public Product FindProduct(string id)
        {
            return this.products.FirstOrDefault(p => p.Id == id);
        }

        public bool DoesProductExists(string id)
        {
            return FindProduct(id) != null;
        }

        public void AddProduct(Product product)
        {
            this.products.Add(product);
        }

        public void EditProduct(Product product)
        {
            var oldProduct = FindProduct(product.Id);
            this.products.Remove(oldProduct);

            this.products.Add(product);
        }
    }
}