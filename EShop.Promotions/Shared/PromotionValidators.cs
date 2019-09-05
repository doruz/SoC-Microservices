using System;
using System.Collections.Generic;

namespace EShop.Promotions.Shared
{
    internal static class PromotionValidators
    {
        public static IEnumerable<string> Validate(this AddPromotionRequestModel promotion)
        {
            if (promotion.StartDate <= DateTime.Today)
            {
                yield return "Start date can't be in the past.";
            }

            if (promotion.StartDate > promotion.EndDate)
            {
                yield return "Start date can't be higher than the end date.";
            }

            if (promotion.NewPrice < 0)
            {
                yield return "Price cannot be negative.";
            }
        }
    }
}