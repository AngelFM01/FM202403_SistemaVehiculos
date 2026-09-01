using Core.Interfaces.Repository;
using Domain.Model;
using MediatR;

namespace Core.Feature.Marcas.Queries;

public sealed class GetMarcasQuery : IRequest<IReadOnlyList<Marca>> { }

public sealed class GetMarcasQueryHandler(IMarcaRepository marcaRepository) : IRequestHandler<GetMarcasQuery, IReadOnlyList<Marca>>
{
    public Task<IReadOnlyList<Marca>> Handle(GetMarcasQuery request, CancellationToken cancellationToken)
        => marcaRepository.GetAllAsync(cancellationToken);
}
