using Microsoft.AspNetCore.Mvc;
using PENANO.Models;
using PENANO.Repositories;

namespace PENANO.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepo;

    // Direct injection via Program.cs
    public AccountController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    public IActionResult Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Collect validation errors if model state fails (e.g., passwords don't match)
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return Json(new { success = false, message = string.Join("<br/>", errors) });
        }

        var newUser = new User
        {
            Username = model.Username,
            Password = model.Password
        };

        if (_userRepo.Create(newUser))
        {
            return Json(new { success = true, message = "Registration successful! You can now log in." });
        }

        return Json(new { success = false, message = "Username is already taken." });
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public IActionResult Login([FromBody] LoginViewModel model) // Or without [FromBody] depending on how data is sent
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Please fill in all required fields." });
        }

        if (_userRepo.ValidateUser(model.Username, model.Password))
        {
           
            HttpContext.Session.SetString("User", model.Username);

            return Json(new { success = true, message = "successfully login" });
        }

        return Json(new { success = false, message = "Invalid username or password." });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}