using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PENANO.Helpers;
using PENANO.Models;
using PENANO.Repositories;
using System.Data;
using static PENANO.Helpers.UserHelper;
using static PENANO.Helpers.AuthHelper;

namespace PENANO.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepo;
    private readonly string? _connectionString;
    private static int attempt = 0;


    public AccountController(IUserRepository userRepo, IConfiguration con)
    {
        _userRepo = userRepo;
        _connectionString = con.GetConnectionString("myLenovoConnectionString");
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

    string tableName = "users";


    // insertUserQuery
    [HttpPost]
    [Route("/Register")]
    public IActionResult Register(string username,string email, string password)
    {
        string[] fields = { "Username", "Email", "Password" };
        string[] userValues = { username, email, password };

        string sqlCommand = RegisterViewModel.buildInsertQuery(
            tableName,
            fields,
            userValues
        );

        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return sendResponse(true, "Registered Successfully");
        }
        catch (Exception ex)
        {
            return sendResponse(false, ex.Message);
        }
    }


    //UpdateUserQuery
    [HttpPost]
    [Route("Update")]
    public IActionResult UpdateUser(string username, string email, string password)
    {
        string[] fields = { "Email", "Password" };
        string[] userValues = { email, password };
        string whereClause = $"Username = '{username}'";

        string query = RegisterViewModel.buildUpdateQuery(
            tableName,
            fields,
            userValues,
            whereClause
        );

        return Json(new { success = true, query });
    }

    //DeleteUserQuery
    [HttpPost]
    [Route("Delete")]
    public IActionResult DeleteUser(string username)
    {

        string whereClause = $"Username = '{username}'";

        string query = RegisterViewModel.buildDeleteQuery(
            tableName,
            whereClause
        );

        return Json(new { success = true, query });
    }



    [HttpPost]
    [Route("/Login")]
    public IActionResult Login(string email, string password)
    {
        if (attempt >= 3) return sendResponse(false, "Too many login attempts try again later");
        
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_CheckUserLogin", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                attempt++;
                return sendResponse(false, "Invalid email or password.");
            }

            attempt = 0;

            return sendResponse(true, "Login successful!");
        }
        catch (Exception ex)
        {
            return sendResponse(false, ex.Message);
        }
    }

}