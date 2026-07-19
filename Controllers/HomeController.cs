using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PENANO.Models;

namespace PENANO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // 3. GET: /Home/Register
        // Displays the registration page
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index");
            }

            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromBody] User model)
        {
            try
            {
                // 1. Guard check for empty payloads


                if (model == null)
                {
                    return Json(new { success = false, errors = new[] { "Invalid registration data payload." } });
                }

                // 2. Validate C# Model Data Annotations
                if (!ModelState.IsValid)
                {
                    var fieldErrors = ModelState.Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(k => k.Key, v => v.Value.Errors.First().ErrorMessage);

                    return Json(new { success = false, fieldErrors = fieldErrors });
                }

                // 3. Mock business logic validation check
                if (model.Email == "admin@gmail.com")
                {
                    return Json(new { success = false, errors = new[] { "This email address is already registered." } });
                }

                // --- Database processing would execute safely here ---

                TempData["res"] = "success";
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                // Log the exception details safely on the server side here (ex.Message)

                // Return a safe, friendly response back to the client UI
                return Json(new
                {
                    success = false,
                    errors = new[] { "An unexpected internal server error occurred while processing your request." }
                });
            }
        }
    }
}
