using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Vehiculos.Commands;

public sealed class UpdateVehiculoCommand : IRequest<bool>
{
    public long VehiculoId { get; set; }
    public long MarcaId { get; set; }
    public string Modelo { get; set; } = null!;
    public int Anio { get; set; }
    public int CantidadPuertas { get; set; }
}

public sealed class UpdateVehiculoCommandHandler(
    IVehiculoRepository vehiculoRepository,
    IMarcaRepository marcaRepository) : IRequestHandler<UpdateVehiculoCommand, bool>
{
    public async Task<bool> Handle(UpdateVehiculoCommand request, CancellationToken cancellationToken)
    {
        var existing = await vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
        if (existing is null) return false;

        if (await marcaRepository.GetByIdAsync(request.MarcaId, cancellationToken) is null)
            throw new KeyNotFoundException("La marca indicada no existe.");

        var vehiculo = new Vehiculo
        {
            VehiculoId = request.VehiculoId,
            MarcaId = request.MarcaId,
            Modelo = request.Modelo,
            Anio = request.Anio,
            CantidadPuertas = request.CantidadPuertas
        };

        await vehiculoRepository.UpdateAsync(vehiculo, cancellationToken);
        return true;
    }
}
