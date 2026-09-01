using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Vehiculos.Commands;

public sealed class CreateVehiculoCommand : IRequest<Vehiculo>
{
    public long MarcaId { get; set; }
    public string Modelo { get; set; } = null!;
    public int Anio { get; set; }
    public int CantidadPuertas { get; set; }
}

public sealed class CreateVehiculoCommandHandler(
    IVehiculoRepository vehiculoRepository,
    IMarcaRepository marcaRepository) : IRequestHandler<CreateVehiculoCommand, Vehiculo>
{
    public async Task<Vehiculo> Handle(CreateVehiculoCommand request, CancellationToken cancellationToken)
    {
        if (await marcaRepository.GetByIdAsync(request.MarcaId, cancellationToken) is null)
            throw new KeyNotFoundException("La marca indicada no existe.");

        var vehiculo = new Vehiculo
        {
            MarcaId = request.MarcaId,
            Modelo = request.Modelo,
            Anio = request.Anio,
            CantidadPuertas = request.CantidadPuertas
        };

        await vehiculoRepository.AddAsync(vehiculo, cancellationToken);
        return vehiculo;
    }
}
