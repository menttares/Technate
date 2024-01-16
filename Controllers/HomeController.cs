using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Technate.Models;
using Technate.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Technate.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private PostgresDataService _database;

    public HomeController(ILogger<HomeController> logger, PostgresDataService database)
    {
        _logger = logger;
        _database = database;
    }


    public IActionResult Index()
    {

        return View();
    }

    [Authorize]
    public IActionResult Courses()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetRetrieveManagedCourses()
    {

        // Получаем почту пользователя из куки
        var EmailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
        _logger.LogInformation(EmailClaim.Value);
        if (EmailClaim != null)
        {
            
            // Вызываем метод для получения курсов
            List<CourseView> courses = _database.GetCoursesByCreatorEmail(EmailClaim.Value);

            // Возвращаем результат в формате JSON
            return Ok(courses);
        }

        // Если что-то пошло не так, возвращаем статус 404
        return NotFound();
    }


    [HttpGet]
    public async Task<IActionResult> GetCoursesUser()
    {

        // Получаем почту пользователя из куки
        var EmailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
        _logger.LogInformation(EmailClaim.Value);
        if (EmailClaim != null)
        {

            // Вызываем метод для получения курсов
            List<CourseView> courses = _database.GetCoursesByUserEmail(EmailClaim.Value);

            // Возвращаем результат в формате JSON
            return Ok(courses);
        }

        // Если что-то пошло не так, возвращаем статус 404
        return NotFound();
    }



    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
