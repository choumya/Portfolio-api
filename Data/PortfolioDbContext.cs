using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Models;

namespace Portfolio.Api.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(
        DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContactMessage> ContactMessages
        => Set<ContactMessage>();

    public DbSet<VisitorCounter> VisitorCounters
    => Set<VisitorCounter>();

    public DbSet<ResumeCounter> ResumeCounters
        => Set<ResumeCounter>();
}