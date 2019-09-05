using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EShop.Catalog
{
    internal static class FunctionsExtensions
    {
        public static async Task<T> DeserializeBody<T>(this HttpRequest request)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
}
