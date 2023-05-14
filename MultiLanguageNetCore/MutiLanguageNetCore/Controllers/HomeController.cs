
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MultiLanguageNetCore.Models;
using MultiLanguageNetCore.Resources;
using System.Diagnostics;

namespace MultiLanguageNetCore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IStringLocalizer<HomeController> _stringLocalizer;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> stringLocalizer, IStringLocalizer<SharedResources> localizer)
    {
        _logger = logger;
        _stringLocalizer = stringLocalizer;
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        var title = _stringLocalizer["Title"];
        var test = _localizer["Phone"];
        return View();
    }

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