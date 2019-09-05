using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Promotions.Shared
{
    public sealed class PromotionsRepository
    {
        private readonly List<Promotion> promotions;

        public PromotionsRepository()
        {
            this.promotions = new List<Promotion>
            {
                new Promotion
                {
                    Id = "05645548-33D8-41DC-B142-A22690EC6EAD",
                    ProductId = "97773443-140A-4028-8C5C-388FEAE12207",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-1),
                },

                new Promotion
                {
                    Id = "72667496-AAA4-4127-B61D-08CF52D4FD99",
                    ProductId = "97773443-140A-4028-8C5C-388FEAE12207",
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(2),
                    NewPrice = 5
                },

                new Promotion
                {
                    Id = "5FF168CD-2C42-46BF-A528-AD1C589CC83F",
                    ProductId = "EF254E34-3CEC-4481-988F-B4C0DFCB0E8B",
                    StartDate = DateTime.Now.AddDays(-2),
                    EndDate = DateTime.Now.AddDays(2),
                    NewPrice = 49.90
                },
            };
        }

        public IEnumerable<Promotion> GetAll()
        {
            return promotions
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();
        }

        public IEnumerable<Promotion> FindOldPromotions()
        {
            return GetAll().Where(p => p.IsExpired);
        }

        public Promotion FindCurrentPromotionForProduct(string productId)
        {
            return promotions
                .Where(p => p.ProductId == productId)
                .FirstOrDefault(p => p.IsApplicableNow);
        }

        public bool DoesPromotionExists(string id)
        {
            return this.promotions.Any(p => p.Id == id);
        }

        public void AddPromotion(Promotion promotion)
        {
            this.promotions.Add(promotion);
        }

        public void DeleteOldPromotions()
        {
            this.promotions.RemoveAll(p => p.IsExpired);
        }

        public void DeletePromotion(string id)
        {
            var promotion = this.promotions.First(p => p.Id == id);
            this.promotions.Remove(promotion);
        }
    }
}