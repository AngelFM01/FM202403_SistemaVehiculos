using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Vehiculos.Queries;

public sealed class GetVehiculosQuery : IRequest<IReadOnlyList<Vehiculo>>
{
    public long? MarcaId { get; set; }
}

public sealed class GetVehiculosQueryHandler(IVehiculoRepository vehiculoRepository) : IRequestHandler<GetVehiculosQuery, IReadOnlyList<Vehiculo>>
{
    public Task<IReadOnlyList<Vehiculo>> Handle(GetVehiculosQuery request, CancellationToken cancellationToken)
        => request.MarcaId.HasValue
            ? vehiculoRepository.GetByMarcaIdAsync(request.MarcaId.Value, cancellationToken)
            : vehiculoRepository.GetAllAsync(cancellationToken);
}
