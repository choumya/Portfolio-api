using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Data;
using Portfolio.Api.Models;
using Portfolio.Api.Services;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly PortfolioDbContext _context;

    private readonly EmailService _emailService;

    public ContactController(
        PortfolioDbContext context,
        EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }
    [HttpPost]
    public async Task<IActionResult> Send(ContactRequest request)
    {
        var contact = new ContactMessage
        {
            Name = request.Name,
            Email = request.Email,
            Subject = request.Subject,
            Message = request.Message,
            CreatedDate = DateTime.UtcNow
        };

        _context.ContactMessages.Add(contact);

        await _context.SaveChangesAsync();
        // await _emailService.SendEmailAsync(
        //     request.Name,
        //     request.Email,
        //     request.Subject,
        //     request.Message);

        return Ok(new
        {
            Message = "Message saved successfully"
        });
    }
}
