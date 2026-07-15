using MentorTech.LandingPage.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorTech.LandingPage.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; }
}
