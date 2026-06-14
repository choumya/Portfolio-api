using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Data;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitorController : ControllerBase
{
    private readonly PortfolioDbContext _context;

    public VisitorController(
        PortfolioDbContext context)
    {
        _context = context;
    }

    [HttpPost("increment")]
    public async Task<IActionResult> Increment()
    {
        var counter =
            _context.VisitorCounters.FirstOrDefault();

        if (counter == null)
        {
            counter = new VisitorCounter
            {
                TotalVisitors = 1
            };

            _context.VisitorCounters.Add(counter);
        }
        else
        {
            counter.TotalVisitors++;
        }

        await _context.SaveChangesAsync();

        return Ok(counter.TotalVisitors);
    }

    [HttpGet]
    public IActionResult GetCount()
    {
        var count =
            _context.VisitorCounters
            .FirstOrDefault()?.TotalVisitors ?? 0;

        return Ok(count);
    }
}