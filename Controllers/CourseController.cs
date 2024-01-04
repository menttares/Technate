using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technate.Models;

namespace Technate.Controllers;
[Authorize]
public class CourseController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public CourseController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    
    public IActionResult Main()
    {

        return View();
    }

    // [HttpPost]
    // public IActionResult CreateCourse([FromBody] CourseViewModel model)
    // {
        
    // }
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
