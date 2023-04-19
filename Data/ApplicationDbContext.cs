using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AgenziaSpedizioni.Models;

namespace AgenziaSpedizioni.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<User> Users { get; set; }
}

