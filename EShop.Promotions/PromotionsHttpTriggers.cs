using System.Linq;
using System.Threading.Tasks;
using EShop.Common;
using EShop.Promotions.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace EShop.Promotions
{
    public class PromotionsHttpTriggers
    {
        private readonly PromotionsRepository repository;

        public PromotionsHttpTriggers(PromotionsRepository repository)
        {
            this.repository = repository;
        }

        [FunctionName("GetPromotions")]
        public IActionResult GetPromotions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promotions")] HttpRequest request)
        {
            return new OkObjectResult(repository.GetAll());
        }

        [FunctionName("GetCurrentPromotion")]
        public IActionResult GetCurrentPromotion([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promotions/{productId}/current")] HttpRequest request, string productId)
        {
            var productPromotion = repository.FindCurrentPromotionForProduct(productId);
            if (productPromotion == null)
            {
                return new NoContentResult();
            }

            return new OkObjectResult(new CurrentPromotionResultModel
            {
                PromotionId = productPromotion.Id,
                Price = productPromotion.NewPrice
            });
        }

        [FunctionName("AddPromotion")]
        public async Task<IActionResult> AddPromotion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promotions")] HttpRequest request)
        {
            var promotionRequest = await request.DeserializeBody<AddPromotionRequestModel>();

            var promotionRequestValidation = promotionRequest.Validate();
            if (promotionRequestValidation.Any())
            {
                return new BadRequestObjectResult(promotionRequestValidation);
            }

            repository.AddPromotion(new Promotion(promotionRequest));

            return ActionResults.Created();
        }

        [FunctionName("DeletePromotion")]
        public IActionResult DeletePromotion([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "promotions/{id}")] HttpRequest request, string id)
        {
            if (!repository.DoesPromotionExists(id))
            {
                return new NotFoundResult();
            }

            repository.DeletePromotion(id);

            return new NoContentResult();
        }
    }
}
