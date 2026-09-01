using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Ventas.Queries;

public sealed class GetVentaByIdQuery : IRequest<Venta?>
{
    public long VentaId { get; set; }
}

public sealed class GetVentaByIdQueryHandler(IVentaRepository ventaRepository) : IRequestHandler<GetVentaByIdQuery, Venta?>
{
    public Task<Venta?> Handle(GetVentaByIdQuery request, CancellationToken cancellationToken)
        => ventaRepository.GetByIdAsync(request.VentaId, cancellationToken);
}
