using System.Diagnostics;
using System.Text.Encodings.Web;
using MentorTech.LandingPage.Data;
using Microsoft.AspNetCore.Mvc;
using MentorTech.LandingPage.Models;
using MentorTech.LandingPage.Services;

namespace MentorTech.LandingPage.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public HomeController(ApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CapturarLead(Lead lead)
    {
        if (ModelState.IsValid)
        {
            _context.Leads.Add(lead);
            _context.SaveChanges();

            var encoder = HtmlEncoder.Default;
            var body = $@"
                <div style='font-family:Arial,sans-serif;max-width:640px;margin:0 auto;padding:24px;background:#f8f9fa;border-radius:8px;'>
                    <h2 style='margin:0 0 16px;color:#0d6efd;'>Novo lead capturado</h2>
                    <p style='margin:0 0 16px;color:#333;'>Um novo lead foi cadastrado na landing page MentorTech.</p>
                    <table style='width:100%;border-collapse:collapse;background:#fff;border:1px solid #e9ecef;'>
                        <tr><td style='padding:10px;border-bottom:1px solid #e9ecef;font-weight:bold;'>Nome</td><td style='padding:10px;border-bottom:1px solid #e9ecef;'>{encoder.Encode(lead.Nome)}</td></tr>
                        <tr><td style='padding:10px;border-bottom:1px solid #e9ecef;font-weight:bold;'>Telefone</td><td style='padding:10px;border-bottom:1px solid #e9ecef;'>{encoder.Encode(lead.Telefone)}</td></tr>
                        <tr><td style='padding:10px;border-bottom:1px solid #e9ecef;font-weight:bold;'>Cargo</td><td style='padding:10px;border-bottom:1px solid #e9ecef;'>{encoder.Encode(lead.CargoAtual)}</td></tr>
                        <tr><td style='padding:10px;font-weight:bold;'>Mensagem</td><td style='padding:10px;'>{encoder.Encode(lead.Mensagem)}</td></tr>
                    </table>
                </div>";

            await _emailService.SendEmailAsync(
                lead.Email,
                "Recebemos seu cadastro na MentorTech",
                body);

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
