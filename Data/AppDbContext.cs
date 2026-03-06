using ContactManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppContext).Assembly,
            type => type.Namespace == nameof(Entities.Configurations));
    }
}
