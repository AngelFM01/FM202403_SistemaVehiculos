using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Marcas.Commands;

public sealed class UpdateMarcaCommand : IRequest<bool>
{
    public long MarcaId { get; set; }
    public string Nombre { get; set; } = null!;
}

public sealed class UpdateMarcaCommandHandler(IMarcaRepository marcaRepository) : IRequestHandler<UpdateMarcaCommand, bool>
{
    public async Task<bool> Handle(UpdateMarcaCommand request, CancellationToken cancellationToken)
    {
        var existing = await marcaRepository.GetByIdAsync(request.MarcaId, cancellationToken);
        if (existing is null) return false;

        if (await marcaRepository.ExistsByNombreAsync(request.Nombre, request.MarcaId, cancellationToken))
            throw new InvalidOperationException("Ya existe una marca con este nombre.");

        var marca = new Marca { MarcaId = request.MarcaId, Nombre = request.Nombre };
        await marcaRepository.UpdateAsync(marca, cancellationToken);
        return true;
    }
}
