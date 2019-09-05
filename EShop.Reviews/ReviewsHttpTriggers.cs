using System.Threading.Tasks;
using EShop.Common;
using EShop.Reviews.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace EShop.Reviews
{
    public class ReviewsHttpTriggers
    {
        private static readonly ReviewsRepository repository = new ReviewsRepository();

        [FunctionName("GetProductReviews")]
        public static IActionResult GetProductReviews([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "reviews/{productId}")] HttpRequest request, string productId)
        {
            var productReviews = repository.FindProductReviews(productId);
            if (productReviews == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(productReviews.Reviews);
        }

        [FunctionName("GetProductReviewsSummary")]
        public static IActionResult GetProductReviewsSummary([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "reviews/{productId}/summary")] HttpRequest request, string productId)
        {
            var productReviews = repository.FindProductReviews(productId);
            if (productReviews == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(productReviews.Summary);
        }

        [FunctionName("AddReview")]
        public async Task<IActionResult> AddReview([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "reviews/{productId}")] HttpRequest request, string productId)
        {
            var reviewModel = await request.DeserializeBody<ReviewRequestModel>();
            var review = new Review(reviewModel.NumberOfStarts, reviewModel.Description);

            repository.AddProductReview(productId, review);

            return new NoContentResult();
        }
    }
}
