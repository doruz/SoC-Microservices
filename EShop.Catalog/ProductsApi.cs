using System.IO;
using System.Threading.Tasks;
using EShop.Catalog.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace EShop.Catalog
{
    public class ProductsApi
    {
        private readonly ProductsRepository repository;

        public ProductsApi(ProductsRepository repository)
        {
            this.repository = repository;
        }

        [FunctionName("GetProducts")]
        public IActionResult GetProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest request)
        {
            var products = this.repository.GetProducts();

            return new OkObjectResult(products);
        }

        [FunctionName("GetProduct")]
        public IActionResult GetProduct([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")] HttpRequest request, string id)
        {
            var product = this.repository.FindProduct(id);

            return product == null
                ? (IActionResult)new NotFoundResult()
                : (IActionResult)new OkObjectResult(product);
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> AddProduct([HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest request)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            var productModel = JsonConvert.DeserializeObject<ProductEditableModel>(requestBody);
            var newProduct = new Product(productModel.Name, productModel.Price);

            this.repository.AddProduct(newProduct);

            return new NoContentResult();
        }

        [FunctionName("EditProduct")]
        public async Task<IActionResult> EditProduct([HttpTrigger(AuthorizationLevel.Function, "put", Route = "products/{id}")] HttpRequest request, string id)
        {
            if (!this.repository.DoesProductExists(id))
            {
                return new NotFoundResult();
            }

            var productModel = await request.DeserializeBody<ProductEditableModel>();
            var editedProduct = new Product(id, productModel.Name, productModel.Price);

            this.repository.EditProduct(editedProduct);

            return new NoContentResult();
        }
    }
}
