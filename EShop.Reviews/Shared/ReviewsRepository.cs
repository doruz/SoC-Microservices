using System.Collections.Generic;
using System.Linq;

namespace EShop.Reviews.Shared
{
    public class ReviewsRepository
    {
        private readonly List<ProductReviews> productsReviews;

        public ReviewsRepository()
        {
            productsReviews = new List<ProductReviews>
            {
                // Clean Code Book
                new ProductReviews("97773443-140A-4028-8C5C-388FEAE12207")
                {
                    Reviews =
                    {
                        new Review(100, "best book ever"),
                        new Review(-100)
                    }
                },

                // JBL GO Speaker
                new ProductReviews("EF254E34-3CEC-4481-988F-B4C0DFCB0E8B")
                {
                    Reviews =
                    {
                        new Review(5, "the sound is really good"),
                        new Review(3, "i like the other one better"),
                        new Review(2)
                    }
                }
            };
        }

        public ProductReviews FindProductReviews(string productId)
        {
            return productsReviews.FirstOrDefault(pr => pr.ProductId == productId);
        }

        public void AddProductReview(string productId, Review review)
        {
            var productReviews = FindProductReviews(productId);
            if (productReviews == null)
            {
                productReviews = new ProductReviews(productId);
                this.productsReviews.Add(productReviews);
            }

            productReviews.AddReview(review);
        }
    }
}