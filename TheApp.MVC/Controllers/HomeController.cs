using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheApp.MVC.Models;

namespace TheApp.MVC.Controllers;

public class HomeController : Controller
{
    //private readonly ILogger<HomeController> _logger;

    //public HomeController(ILogger<HomeController> logger)
    //
        //_logger = logger;
    //}

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult NoAccess()
    {
        return View();
    }

    public IActionResult About()
    {
        var infoModel = new List<InfoModel>()
        {
            new()
            {
                Title = "Info Model Title",
                Description = "Info Model Description",
                Tags = ["#dental", "#studio", "app"]
            },
            new()
            {
                Title = "Info Model Test",
                Description = "Info Model Description Two",
                Tags = ["#First Tag Test", "#Second Tag Test"]
            }
        };

        return View(infoModel);
    }

    public IActionResult Privacy()
    {
        var model = new List<Person>()
        {
            new()
            {
                FristNmae = "macio",
                LastName = "haki"
            },
            new()
            {
                FristNmae ="basix",
                LastName = "patix"
            },
        };


        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}