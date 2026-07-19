using System.Diagnostics;
using MentorTech.LandingPage.Data;
using Microsoft.AspNetCore.Mvc;
using MentorTech.LandingPage.Models;

namespace MentorTech.LandingPage.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CapturarLead(Lead lead)
    {
        if (ModelState.IsValid)
        {
            _context.Leads.Add(lead);
            _context.SaveChanges();

            return RedirectToAction(nameof(Sucesso));
        }

        return View("Index", lead);
    }

    public IActionResult Sucesso()
    {
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
