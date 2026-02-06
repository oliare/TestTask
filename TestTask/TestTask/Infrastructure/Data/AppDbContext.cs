using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<IncidentEntity> Incidents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ContactEntity>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<AccountEntity>()
            .HasIndex(a => a.Name)
            .IsUnique();
    }
}