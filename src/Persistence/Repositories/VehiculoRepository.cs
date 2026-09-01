using Core.Interfaces.Repository;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public sealed class VehiculoRepository(AppDbContext context) : IVehiculoRepository
{
    public async Task<IReadOnlyList<Vehiculo>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Vehiculos.AsNoTracking().OrderBy(x => x.MarcaId).ThenBy(x => x.Modelo).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Vehiculo>> GetByMarcaIdAsync(long marcaId, CancellationToken cancellationToken = default) =>
        await context.Vehiculos.AsNoTracking().Where(x => x.MarcaId == marcaId).OrderBy(x => x.Modelo).ToListAsync(cancellationToken);

    public Task<Vehiculo?> GetByIdAsync(long vehiculoId, CancellationToken cancellationToken = default) =>
        context.Vehiculos.AsNoTracking().FirstOrDefaultAsync(x => x.VehiculoId == vehiculoId, cancellationToken);

    public async Task AddAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default)
    {
        await context.Vehiculos.AddAsync(vehiculo, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default)
    {
        context.Vehiculos.Update(vehiculo);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(long vehiculoId, CancellationToken cancellationToken = default)
    {
        var vehiculo = await context.Vehiculos.FindAsync([vehiculoId], cancellationToken);
        if (vehiculo is null) return false;
        context.Vehiculos.Remove(vehiculo);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
