using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Technate.Models;
using Technate.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
namespace Technate.Controllers;

[Authorize]
[Route("Course")]
public class CourseController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private PostgresDataService _database;

    public CourseController(ILogger<HomeController> logger, PostgresDataService database)
    {
        _logger = logger;
        _database = database;
    }



    [Route("Course/{codeCourse}")]
    public IActionResult Course(int codeCourse)
    {
        CourseView course = _database.GetCourseByCode(codeCourse);
        return View(course);
    }


    [Route("Main/{codeCourse}")]
    public IActionResult Main(int codeCourse)
    {
        CourseView course = _database.GetCourseByCode(codeCourse);
        return PartialView(course);
    }




    [HttpPost]
    [Route("CreateCourse")]
    public async Task<IActionResult> CreateCourse([FromBody] CourseViewModel model)
    {
        // Извлекаем идентификатор пользователя из претензии
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            // Вызываем метод CreateCourseAsync, передавая идентификатор пользователя
            int status = _database.CreateCourse(model.courseName, model.courseSubject, userId);

            // Обрабатываем результат, например, возвращаем статус 200 в случае успеха
            if (status != -1)
            {
                return Ok(status);
            }
        }

        // Если что-то пошло не так, возвращаем статус 404
        return StatusCode(404);
    }


    [HttpPost]
    [Route("InviteCourse")]
    public async Task<IActionResult> InviteCourse(int courseCode)
    {
        // Извлекаем идентификатор пользователя из претензии
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            // Вызываем метод CreateCourseAsync, передавая идентификатор пользователя
            int status = _database.AddUserToCourse(userId, courseCode);

            // Обрабатываем результат, например, возвращаем статус 200 в случае успеха
            if (status != -1)
            {
                return Ok(status);
            }
        }

        // Если что-то пошло не так, возвращаем статус 404
        return StatusCode(404);
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
