using System.Diagnostics;
using MentorTech.LandingPage.Data;
using MentorTech.LandingPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CapturarLead(Lead model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            model.DataCaptura = DateTime.Now;
            await _context.Leads.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Sucesso));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Nao foi possivel salvar seu cadastro no momento. Tente novamente.");
            return View("Index", model);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao processar seu cadastro.");
            return View("Index", model);
        }
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
