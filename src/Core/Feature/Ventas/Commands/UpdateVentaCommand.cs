using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Ventas.Commands;

public sealed class UpdateVentaCommand : IRequest<bool>
{
    public long VentaId { get; set; }
    public double TotalVenta { get; set; }
    public int Cantidad { get; set; }
}

public sealed class UpdateVentaCommandHandler(IVentaRepository ventaRepository) : IRequestHandler<UpdateVentaCommand, bool>
{
    public async Task<bool> Handle(UpdateVentaCommand request, CancellationToken cancellationToken)
    {
        var existing = await ventaRepository.GetByIdAsync(request.VentaId, cancellationToken);
        if (existing is null) return false;

        var venta = new Venta
        {
            VentaId = request.VentaId,
            VehiculoId = existing.VehiculoId,
            TotalVenta = request.TotalVenta,
            Cantidad = request.Cantidad
        };

        await ventaRepository.UpdateAsync(venta, cancellationToken);
        return true;
    }
}
