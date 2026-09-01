using Domain.Model;

namespace Core.Interfaces.Repository;

public interface IVentaRepository
{
    Task<IReadOnlyList<Venta>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Venta?> GetByIdAsync(long ventaId, CancellationToken cancellationToken = default);
    Task<Venta?> GetByVehiculoIdAsync(long vehiculoId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByVehiculoIdAsync(long vehiculoId, CancellationToken cancellationToken = default);
    Task AddAsync(Venta venta, CancellationToken cancellationToken = default);
    Task UpdateAsync(Venta venta, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long ventaId, CancellationToken cancellationToken = default);
}
