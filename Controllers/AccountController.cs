using Microsoft.AspNetCore.Mvc;
using PENANO.Models;
using PENANO.Repositories;

namespace PENANO.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepo;

    public AccountController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }



    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    string tableName = "User";


    // POST: /Account/Register
    [HttpPost]
    [Route("/Register")]

    public IActionResult Register(string username, string password)
    {

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return Json(new { success = false, message = "Username and password are required." });
        }

        string[] fields = { "Username", "Password" };
        string[] userValues = { username, password };

        string query = RegisterViewModel.GenerateInsertQuery(
            tableName,
            fields,
            userValues
        );

        return Json(new { success = true, query });
    }


    
    




    // POST: /Account/UpdateUser
    [HttpPost]
    [Route("Update")]
    public IActionResult UpdateUser(string username, string email, string password)
    {
        string[] fields = { "Email", "Password" };
        string[] userValues = { email, password };
        string whereClause = $"Username = '{username}'";

        string query = RegisterViewModel.GenerateUpdateQuery(
            tableName,
            fields,
            userValues,
            whereClause
        );

        return Json(new { success = true, query });
    }

    // POST: /Account/DeleteUser
    [HttpPost]
    [Route("Delete")]
    public IActionResult DeleteUser(string username)
    {

        string whereClause = $"Username = '{username}'";

        string query = RegisterViewModel.GenerateDeleteQuery(
            tableName,
            whereClause
        );

        return Json(new { success = true, query });
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




}