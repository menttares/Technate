using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Technate.Models;
using Technate.Services;
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
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
