using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Marcas.Commands;

public sealed class CreateMarcaCommand : IRequest<Marca>
{
    public string Nombre { get; set; } = null!;
}

public sealed class CreateMarcaCommandHandler(IMarcaRepository marcaRepository) : IRequestHandler<CreateMarcaCommand, Marca>
{
    public async Task<Marca> Handle(CreateMarcaCommand request, CancellationToken cancellationToken)
    {
        if (await marcaRepository.ExistsByNombreAsync(request.Nombre, cancellationToken: cancellationToken))
            throw new InvalidOperationException("Ya existe una marca con este nombre.");

        var marca = new Marca { Nombre = request.Nombre };
        await marcaRepository.AddAsync(marca, cancellationToken);
        return marca;
    }
}
