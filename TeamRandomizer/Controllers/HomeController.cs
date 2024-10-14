using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TeamRandomizer.Models;

namespace TeamRandomizer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new NameShuffler());
    }

    // Take posted data from the form on the Index page, process it and send it to the view for the Teams page
    [HttpPost]
    public IActionResult Teams(NameShuffler TeamEntryData)
    {
        if (ModelState.IsValid)
        {
            NameShuffler.ProcessData(TeamEntryData);
            return View("Teams", TeamEntryData);
        }

        NameShuffler.ProcessData(TeamEntryData);
        return View("Index", TeamEntryData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
