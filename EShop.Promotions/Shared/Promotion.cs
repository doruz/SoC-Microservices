using System;

namespace EShop.Promotions.Shared
{
    public class Promotion
    {
        public Promotion()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Promotion(AddPromotionRequestModel promotionRequest)
            : this()
        {
            StartDate = promotionRequest.StartDate;
            EndDate = promotionRequest.EndDate;
            NewPrice = promotionRequest.NewPrice;
        }

        public string Id { get; set; }

        public string ProductId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double NewPrice { get; set; }

        public bool IsApplicableNow => StartDate.Date <= DateTime.Today && DateTime.Today <= EndDate.Date;

        public bool IsExpired => EndDate < DateTime.Now;
    }
}
