using Core.Interfaces.Repository;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public sealed class VentaRepository(AppDbContext context) : IVentaRepository
{
    public async Task<IReadOnlyList<Venta>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Ventas.AsNoTracking().OrderByDescending(x => x.VentaId).ToListAsync(cancellationToken);

    public Task<Venta?> GetByIdAsync(long ventaId, CancellationToken cancellationToken = default) =>
        context.Ventas.AsNoTracking().FirstOrDefaultAsync(x => x.VentaId == ventaId, cancellationToken);

    public Task<Venta?> GetByVehiculoIdAsync(long vehiculoId, CancellationToken cancellationToken = default) =>
        context.Ventas.AsNoTracking().FirstOrDefaultAsync(x => x.VehiculoId == vehiculoId, cancellationToken);

    public Task<bool> ExistsByVehiculoIdAsync(long vehiculoId, CancellationToken cancellationToken = default) =>
        context.Ventas.AnyAsync(x => x.VehiculoId == vehiculoId, cancellationToken);

    public async Task AddAsync(Venta venta, CancellationToken cancellationToken = default)
    {
        await context.Ventas.AddAsync(venta, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Venta venta, CancellationToken cancellationToken = default)
    {
        context.Ventas.Update(venta);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(long ventaId, CancellationToken cancellationToken = default)
    {
        var venta = await context.Ventas.FindAsync([ventaId], cancellationToken);
        if (venta is null) return false;
        context.Ventas.Remove(venta);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
