using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Technate.Models;
using Technate.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
namespace Technate.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private PostgresDataService _database;

    public LoginController(ILogger<LoginController> logger, PostgresDataService database)
    {
        _logger = logger;
        _database = database;
    }

    [HttpGet]
    public IActionResult Registration() => View();
    [HttpGet]
    public IActionResult Authorization() => View();

    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegisterViewModel model)
    {
        _logger.LogInformation("[регистрация]");

        if (!_database.UserExists(model.username))
        {
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, model.email),
                new Claim("username", model.username)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            await _database.AddUserAsync(model.username, model.email, model.password);

            return Ok();
        }

        return StatusCode(405);
    }

    [HttpPost]
    public async Task<IActionResult> Authorization([FromBody] LoginViewModel model)
    {
        _logger.LogInformation("[авторизация]");

        if (_database.UserExists(model.email) && _database.VerifyPassword(model.email, model.password))
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, model.email),
                new Claim("username", _database.GetUsernameByEmail(model.email))
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            
            return Ok();
        }
        

        return StatusCode(405);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
