using System;

namespace EShop.Promotions.Shared
{
    public class AddPromotionRequestModel
    {
        public string ProductId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double NewPrice { get; set; }
    }
}
