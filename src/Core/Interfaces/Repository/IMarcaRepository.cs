using Domain.Model;

namespace Core.Interfaces.Repository;

public interface IMarcaRepository
{
    Task<IReadOnlyList<Marca>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Marca?> GetByIdAsync(long marcaId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNombreAsync(string nombre, long? excludingMarcaId = null, CancellationToken cancellationToken = default);
    Task AddAsync(Marca marca, CancellationToken cancellationToken = default);
    Task UpdateAsync(Marca marca, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long marcaId, CancellationToken cancellationToken = default);
}
