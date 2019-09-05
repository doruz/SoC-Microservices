using System.Linq;
using EShop.Promotions.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EShop.Promotions
{
    public class PromotionsTimeTriggers
    {
        private readonly PromotionsRepository repository;

        public PromotionsTimeTriggers(PromotionsRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// This will run daily and delete old promotions.
        /// </summary>
        [FunctionName("DeleteOldPromotions")]
        public void Run([TimerTrigger("0 0 * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger logger)
        {
            var oldPromotions = this.repository.FindOldPromotions();

            if (oldPromotions.Any())
            {
                var oldPromotionsAsJson = JsonConvert.SerializeObject(oldPromotions, Formatting.Indented);
                logger.LogInformation($"Following promotions are removed: {oldPromotionsAsJson}");
                this.repository.DeleteOldPromotions();
            }
            else
            {
                logger.LogInformation("There aren't any old promotions to be removed.");
            }
        }
    }
}
