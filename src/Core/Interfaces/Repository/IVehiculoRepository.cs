using Domain.Model;

namespace Core.Interfaces.Repository;

public interface IVehiculoRepository
{
    Task<IReadOnlyList<Vehiculo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Vehiculo>> GetByMarcaIdAsync(long marcaId, CancellationToken cancellationToken = default);
    Task<Vehiculo?> GetByIdAsync(long vehiculoId, CancellationToken cancellationToken = default);
    Task AddAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default);
    Task UpdateAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long vehiculoId, CancellationToken cancellationToken = default);
}
