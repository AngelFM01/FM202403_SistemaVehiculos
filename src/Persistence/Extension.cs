using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence;

public static class Extension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        IConfiguration configuration;
        using (ServiceProvider provider = services.BuildServiceProvider())
            configuration = provider.GetRequiredService<IConfiguration>();

        var connectionString = configuration["sql:cx"]
            ?? throw new InvalidOperationException("No se configuro la cadena de conexion 'sql:cx'.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IMarcaRepository, MarcaRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["sql:cx"]
            ?? throw new InvalidOperationException("No se configuro la cadena de conexion 'sql:cx'.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IMarcaRepository, MarcaRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();

        return services;
    }
}
