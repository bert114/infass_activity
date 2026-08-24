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


    public class AuthHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "LoginAttempts";
        private const int MaxAttempts = 3;

        public AuthHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public bool IsAccountBlocked()
        {
            int attempts = Session.GetInt32(SessionKey) ?? 0;
            return attempts >= MaxAttempts;
        }

        public void ResetAttempts()
        {
            Session.Remove(SessionKey);
        }

        public string RecordFailedAttemptAndGetMessage()
        {
            int currentAttempts = (Session.GetInt32(SessionKey) ?? 0) + 1;
            Session.SetInt32(SessionKey, currentAttempts);

            int remaining = MaxAttempts - currentAttempts;

            return remaining > 0
                ? $"Invalid email or password. You have {remaining} attempt(s) left."
                : "Invalid email or password. Maximum attempts reached. Account blocked.";
        }
    }

}