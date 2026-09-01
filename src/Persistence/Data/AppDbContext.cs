using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    public DbSet<Venta> Ventas => Set<Venta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
