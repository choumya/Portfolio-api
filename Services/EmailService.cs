using System.Net;
using System.Net.Mail;

namespace Portfolio.Api.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(
        string name,
        string email,
        string subject,
        string message)
    {
        var senderEmail =
            _configuration["EmailSettings:SenderEmail"];

        var senderPassword =
            _configuration["EmailSettings:SenderPassword"];

        using var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials =
                new NetworkCredential(
                    senderEmail,
                    senderPassword),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail!),
            Subject = $"Portfolio Contact - {subject}",
            Body =
                $"Name: {name}\n" +
                $"Email: {email}\n\n" +
                $"Message:\n{message}",
            IsBodyHtml = false
        };

        mailMessage.To.Add("choumya0703@gmail.com");

        await smtp.SendMailAsync(mailMessage);
    }
}