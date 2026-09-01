using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Ventas.Commands;

// Caso de uso: Realizar Venta Vehiculo
public sealed class CreateVentaCommand : IRequest<Venta>
{
    public long VehiculoId { get; set; }
    public double TotalVenta { get; set; }
    public int Cantidad { get; set; }
}

public sealed class CreateVentaCommandHandler(
    IVentaRepository ventaRepository,
    IVehiculoRepository vehiculoRepository) : IRequestHandler<CreateVentaCommand, Venta>
{
    public async Task<Venta> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
    {
        if (await vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken) is null)
            throw new KeyNotFoundException("El vehiculo indicado no existe.");

        if (await ventaRepository.ExistsByVehiculoIdAsync(request.VehiculoId, cancellationToken))
            throw new InvalidOperationException("El vehiculo ya tiene una venta registrada.");

        var venta = new Venta
        {
            VehiculoId = request.VehiculoId,
            TotalVenta = request.TotalVenta,
            Cantidad = request.Cantidad
        };

        await ventaRepository.AddAsync(venta, cancellationToken);
        return venta;
    }
}
