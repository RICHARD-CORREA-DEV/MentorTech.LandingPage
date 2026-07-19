using System.Net;
using System.Net.Mail;

namespace MentorTech.LandingPage.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var host = GetRequiredSetting("EmailSettings:Host");
        var fromAddress = GetRequiredSetting("EmailSettings:FromAddress");
        var password = GetRequiredSetting("EmailSettings:Password");
        var portSetting = GetRequiredSetting("EmailSettings:Port");

        if (!int.TryParse(portSetting, out var port))
        {
            throw new InvalidOperationException("EmailSettings:Port invalido.");
        }

        using var client = new SmtpClient(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(fromAddress, password)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(fromAddress),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);
        await client.SendMailAsync(message);
    }

    private string GetRequiredSetting(string key)
    {
        var value = _configuration[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Configuracao obrigatoria ausente: {key}");
        }

        return value;
    }
}
