using Core.Interfaces.Repository;
using MediatR;

namespace Core.Feature.Marcas.Commands;

public sealed class DeleteMarcaCommand : IRequest<bool>
{
    public long MarcaId { get; set; }
}

public sealed class DeleteMarcaCommandHandler(IMarcaRepository marcaRepository) : IRequestHandler<DeleteMarcaCommand, bool>
{
    public Task<bool> Handle(DeleteMarcaCommand request, CancellationToken cancellationToken)
        => marcaRepository.DeleteAsync(request.MarcaId, cancellationToken);
}
