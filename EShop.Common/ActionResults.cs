using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Common
{
    public static class ActionResults
    {
        public static IActionResult Created()
        {
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        public static IActionResult Created(string location, object value)
        {
            return new CreatedResult(location, value);
        }
    }
}