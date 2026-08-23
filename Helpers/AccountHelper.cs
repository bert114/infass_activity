using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PENANO.Helpers
{
    public static class UserHelper
    {
        public static IActionResult sendResponse(bool success, string message, object data = null)
        {
            return new JsonResult(new
            {
                success = success,
                message = message,
                data = data
            });
        }
    }
}