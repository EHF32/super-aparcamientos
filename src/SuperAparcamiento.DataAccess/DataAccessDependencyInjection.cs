using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperAparcamiento.DataAccess.Persistence;
using SuperAparcamiento.DataAccess.Repositories;
using SuperAparcamiento.DataAccess.Repositories.Interfaces;

namespace SuperAparcamiento.DataAccess;

/// <summary>
/// Encapsula la configuración de la inyección de dependencias de la capa de acceso a datos
/// </summary>
public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddDatabase();

        services.AddRepositories();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AparcamientoDbContext>(options =>
        {
            options.UseSqlite("DataSource=file::memory:?cache=shared");
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<ITipoVehiculoRepository, TipoVehiculoRepository>();
        services.AddScoped<IEstanciaRepository, EstanciaRepository>();
    }


}
