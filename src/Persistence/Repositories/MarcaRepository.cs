using Core.Interfaces.Repository;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public sealed class MarcaRepository(AppDbContext context) : IMarcaRepository
{
    public async Task<IReadOnlyList<Marca>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Marcas.AsNoTracking().OrderBy(x => x.Nombre).ToListAsync(cancellationToken);

    public Task<Marca?> GetByIdAsync(long marcaId, CancellationToken cancellationToken = default) =>
        context.Marcas.AsNoTracking().FirstOrDefaultAsync(x => x.MarcaId == marcaId, cancellationToken);

    public Task<bool> ExistsByNombreAsync(string nombre, long? excludingMarcaId = null, CancellationToken cancellationToken = default) =>
        context.Marcas.AnyAsync(x => x.Nombre == nombre && (!excludingMarcaId.HasValue || x.MarcaId != excludingMarcaId), cancellationToken);

    public async Task AddAsync(Marca marca, CancellationToken cancellationToken = default)
    {
        await context.Marcas.AddAsync(marca, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Marca marca, CancellationToken cancellationToken = default)
    {
        context.Marcas.Update(marca);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(long marcaId, CancellationToken cancellationToken = default)
    {
        var marca = await context.Marcas.FindAsync([marcaId], cancellationToken);
        if (marca is null) return false;
        context.Marcas.Remove(marca);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
