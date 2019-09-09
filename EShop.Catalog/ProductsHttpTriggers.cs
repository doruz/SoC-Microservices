using System.Threading.Tasks;
using EShop.Catalog.Shared;
using EShop.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EShop.Catalog
{
    public class ProductsHttpTriggers
    {
        private readonly ProductsRepository repository;

        public ProductsHttpTriggers(ProductsRepository repository)
        {
            this.repository = repository;
        }

        [FunctionName("GetProducts")]
        public IActionResult GetProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest request)
        {
            var products = repository.GetProducts();

            return new OkObjectResult(products);
        }

        [FunctionName("FindProduct")]
        public IActionResult FindProduct([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")] HttpRequest request, string id)
        {
            var product = repository.FindProduct(id);

            return product == null
                ? (IActionResult)new NotFoundResult()
                : (IActionResult)new OkObjectResult(product);
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> AddProduct([HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest request)
        {
            var productModel = await request.DeserializeBody<ProductEditableModel>();
            var newProduct = new Product(productModel.Name, productModel.Price);

            repository.AddProduct(newProduct);

            return ActionResults.Created($"/api/products/{newProduct.Id}", newProduct);
        }

        [FunctionName("EditProduct")]
        public async Task<IActionResult> EditProduct([HttpTrigger(AuthorizationLevel.Function, "put", Route = "products/{id}")] HttpRequest request, string id)
        {
            if (!repository.DoesProductExists(id))
            {
                return new NotFoundResult();
            }

            var productModel = await request.DeserializeBody<ProductEditableModel>();
            var editedProduct = new Product(id, productModel.Name, productModel.Price);

            repository.EditProduct(editedProduct);

            return new NoContentResult();
        }
    }
}
