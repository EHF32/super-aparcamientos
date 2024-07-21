using Microsoft.Extensions.DependencyInjection;
using SuperAparcamiento.Logic.MappingProfiles;
using SuperAparcamiento.Logic.Services;
using SuperAparcamiento.Logic.Services.Interfaces;

namespace SuperAparcamiento.Logic;

public static class LogicDependencyInjection
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddServices();

        services.AddAutoMapperConfiguration();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IVehiculoService, VehiculoService>();
        services.AddScoped<IEstanciaService, EstanciaService>();
        services.AddScoped<IInformeService, InformeService>();
    }

    private static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }
}
