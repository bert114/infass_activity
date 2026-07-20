using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PENANO.Models;
using System.Reflection;
using Newtonsoft.Json.Linq; 




namespace PENANO.Controllers
{
    public class AccountController : Controller
    {
        // 1. GET: /Account/Login
        // This just displays the empty login page when the user navigates to it
        [HttpGet]
        public IActionResult Login()
        {
            // If a user is already logged in, send them straight to the homepage
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new User());
        }

        // 2. POST: /Account/Login
        // This handles processing the data when the user clicks the "Sign In" button
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects your app from Cross-Site Request Forgery (CSRF) attacks
        public IActionResult Login(User model)
        {
            // First, check if the validation rules in your Model (like [Required]) pass
            if (!ModelState.IsValid)
            {
                // If something is invalid, send them back to the login page 
                // The form will automatically display the error messages
                return View(model);
            }

            // --- HARDCODED CREDENTIALS TEST ---
            // Replace this block later with your actual database/Identity authentication logic!
            if (model.Email == "admin@gmail.com" && model.Password == "123")
            {
                // Success! Redirect them to your main landing/portfolio page

                TempData["res"] = "success";
                return RedirectToAction("Index", "Home");
            }

            // If the code reaches here, the email or password was incorrect
            ModelState.AddModelError(string.Empty, "Invalid email address or password.");

            // Return the model back to the view so their typed email stays in the box
            return View(model);
        }

        // 3. GET: /Account/Logout
        public IActionResult Logout()
        {
            // Your logout logic will go here later
            return RedirectToAction("Login");
        }




        [HttpPost]
        public JsonResult ValidateUser(string username, string password)
        {
            // Now you can use the raw parameters directly!
            if (username == "jan" && password == "123")
            {
                TempData["res"] = "success";
                return Json(new { success = true,  username });
            }

            return Json(new { success = false, message = "Invalid credentials." });
        }

        public class LoginResponse
        {
            public bool success { get; set; }
            public string username { get; set; }
            public string message { get; set; }
        }


    }
}