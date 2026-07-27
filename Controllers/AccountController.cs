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
    [Route("newuser")]
    public IActionResult Register(string username, string password)
    {

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return Json(new { success = false, message = "Username and password are required." });
        }

        

        string[] fields = { "Username", "Password" };
        string[] userValues = { username, password };


        string query = PENANO.Models.RegisterViewModel.DisplayQuery("Users", userValues, fields);








        _userRepo.AddInMemoryUser(username, password);

        HttpContext.Session.SetString("User", username);

        return Json(new { success = true, message = query });

        
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public IActionResult Login(LoginViewModel model) 
    {
        if (model == null || !ModelState.IsValid)
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