using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Reviews.Shared
{
    public class ReviewsSummary
    {
        public ReviewsSummary(IEnumerable<Review> reviews)
        {
            TotalNumberOfReviews = reviews.Count();
            AverageScore = Math.Round(reviews.Average(review => review.NumberOfStarts), 2);
        }

        public int TotalNumberOfReviews { get; }

        public double AverageScore { get; }
    }
}