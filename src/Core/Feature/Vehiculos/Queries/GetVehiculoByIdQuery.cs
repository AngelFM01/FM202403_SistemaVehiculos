using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Vehiculos.Queries;

public sealed class GetVehiculoByIdQuery : IRequest<Vehiculo?>
{
    public long VehiculoId { get; set; }
}

public sealed class GetVehiculoByIdQueryHandler(IVehiculoRepository vehiculoRepository) : IRequestHandler<GetVehiculoByIdQuery, Vehiculo?>
{
    public Task<Vehiculo?> Handle(GetVehiculoByIdQuery request, CancellationToken cancellationToken)
        => vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
}
