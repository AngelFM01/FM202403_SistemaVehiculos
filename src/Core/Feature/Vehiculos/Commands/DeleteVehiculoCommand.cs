using Core.Interfaces.Repository;
using MediatR;

namespace Core.Feature.Vehiculos.Commands;

public sealed class DeleteVehiculoCommand : IRequest<bool>
{
    public long VehiculoId { get; set; }
}

public sealed class DeleteVehiculoCommandHandler(IVehiculoRepository vehiculoRepository) : IRequestHandler<DeleteVehiculoCommand, bool>
{
    public Task<bool> Handle(DeleteVehiculoCommand request, CancellationToken cancellationToken)
        => vehiculoRepository.DeleteAsync(request.VehiculoId, cancellationToken);
}
