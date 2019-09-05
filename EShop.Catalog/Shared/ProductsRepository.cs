using System.Collections.Generic;
using System.Linq;

namespace EShop.Catalog.Shared
{
    public sealed class ProductsRepository
    {
        private readonly List<Product> products;

        public ProductsRepository()
        {
            products = new List<Product>
            {
                new Product(
                    "97773443-140A-4028-8C5C-388FEAE12207",
                    "Clean Code Book", 
                    10.5),

                new Product(
                    "EF254E34-3CEC-4481-988F-B4C0DFCB0E8B", 
                    "JBL GO Speaker", 
                    100)
            };
        }

        public IEnumerable<Product> GetProducts()
        {
            return products.OrderBy(p => p.Name).ToList();
        }

        public Product FindProduct(string id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public bool DoesProductExists(string id)
        {
            return FindProduct(id) != null;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void EditProduct(Product product)
        {
            var oldProduct = FindProduct(product.Id);
            products.Remove(oldProduct);

            products.Add(product);
        }
    }
}