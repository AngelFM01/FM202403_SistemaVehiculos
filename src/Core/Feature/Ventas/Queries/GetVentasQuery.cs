using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Ventas.Queries;

public sealed class GetVentasQuery : IRequest<IReadOnlyList<Venta>> { }

public sealed class GetVentasQueryHandler(IVentaRepository ventaRepository) : IRequestHandler<GetVentasQuery, IReadOnlyList<Venta>>
{
    public Task<IReadOnlyList<Venta>> Handle(GetVentasQuery request, CancellationToken cancellationToken)
        => ventaRepository.GetAllAsync(cancellationToken);
}
