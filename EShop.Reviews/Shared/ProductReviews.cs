using System.Collections.Generic;

namespace EShop.Reviews.Shared
{
    public class ProductReviews
    {
        public ProductReviews(string productId)
        {
            ProductId = productId;
            Reviews = new List<Review>();
        }

        public string ProductId { get; }

        public List<Review> Reviews { get; }

        public ReviewsSummary Summary => new ReviewsSummary(Reviews);

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}