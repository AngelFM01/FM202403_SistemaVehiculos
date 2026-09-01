using Core.Interfaces.Repository;
using MediatR;

namespace Core.Feature.Ventas.Commands;

public sealed class DeleteVentaCommand : IRequest<bool>
{
    public long VentaId { get; set; }
}

public sealed class DeleteVentaCommandHandler(IVentaRepository ventaRepository) : IRequestHandler<DeleteVentaCommand, bool>
{
    public Task<bool> Handle(DeleteVentaCommand request, CancellationToken cancellationToken)
        => ventaRepository.DeleteAsync(request.VentaId, cancellationToken);
}
