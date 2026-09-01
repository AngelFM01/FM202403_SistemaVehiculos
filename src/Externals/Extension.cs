using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Externals;

public static class Extension
{
    public static IServiceCollection AddExternals(this IServiceCollection services)
    {
        IConfiguration configuration;
        using (ServiceProvider provider = services.BuildServiceProvider())
            configuration = provider.GetRequiredService<IConfiguration>();

        // Aqui se registrarian HttpClients tipados hacia otros microservicios.

        return services;
    }
}
