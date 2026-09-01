using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Marcas.Queries;

public sealed class GetMarcaByIdQuery : IRequest<Marca?>
{
    public long MarcaId { get; set; }
}

public sealed class GetMarcaByIdQueryHandler(IMarcaRepository marcaRepository) : IRequestHandler<GetMarcaByIdQuery, Marca?>
{
    public Task<Marca?> Handle(GetMarcaByIdQuery request, CancellationToken cancellationToken)
        => marcaRepository.GetByIdAsync(request.MarcaId, cancellationToken);
}
