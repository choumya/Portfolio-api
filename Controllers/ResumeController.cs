using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Data;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly PortfolioDbContext _context;

    public ResumeController(
        PortfolioDbContext context)
    {
        _context = context;
    }

    [HttpPost("download")]
    public async Task<IActionResult> Download()
    {
        var counter =
            _context.ResumeCounters.FirstOrDefault();

        if (counter == null)
        {
            counter = new ResumeCounter
            {
                TotalDownloads = 1
            };

            _context.ResumeCounters.Add(counter);
        }
        else
        {
            counter.TotalDownloads++;
        }

        await _context.SaveChangesAsync();

        return Ok(counter.TotalDownloads);
    }

    [HttpGet]
    public IActionResult GetCount()
    {
        var count =
            _context.ResumeCounters
            .FirstOrDefault()?.TotalDownloads ?? 0;

        return Ok(count);
    }
}