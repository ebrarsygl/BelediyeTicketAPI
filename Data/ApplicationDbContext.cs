using Microsoft.EntityFrameworkCore;
using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}