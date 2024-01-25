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


        int idUser = _database.AddUser(model.username, model.email, model.password);

        if (idUser == -1)
            return StatusCode(400, "Уже есть такой пользователь с почтой");
        else if (idUser == -2)
        {
            return StatusCode(500, "Не удалось создать пользователя");
        }

        var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, model.email),
                new Claim("username", model.username),
                new Claim(ClaimTypes.NameIdentifier, idUser.ToString())
                };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Authorization([FromBody] LoginViewModel model)
    {
        _logger.LogInformation("[авторизация]");


        int status = _database.VerifyPassword(model.email, model.password);
        if (status == -1) {
            return StatusCode(400, "Неверный пароль");
        }
        else if (status == -2) {
            return StatusCode(404, "Нет такого пользователя с почтой");
        }

        int idUser = _database.UserExists(model.email);



            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, model.email),
                new Claim("username", _database.GetUsernameByEmail(model.email)),
                new Claim(ClaimTypes.NameIdentifier, idUser.ToString())
                };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return Ok();




    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
