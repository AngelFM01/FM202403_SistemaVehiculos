using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Extension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // Rama Core: configuracion previa a la implementacion de MediatR.
        // El registro de MediatR (handlers de Commands/Queries) se agrega en la rama CQRS.
        return services;
    }
}
