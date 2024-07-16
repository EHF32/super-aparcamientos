using Microsoft.EntityFrameworkCore;
using SuperAparcamiento.Model.Common;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    /// <summary>
    /// Introduce datos iniciales en la base de datos
    /// </summary>
    public static ModelBuilder SeedDatabase(this ModelBuilder modelBuilder)
    {
        // Incializa los tipos de vehículos iniciales
        modelBuilder.Entity<TipoVehiculo>().HasData(
             new TipoVehiculo()
             {
                 Id = Constants.TIPO_VEHICULO_OFICIAL,
                 Nombre = "Vehículo Oficial",
                 PrecioMinuto = 0,
                 Vehiculos = [],
                 EsExterno = false,
             },
             new TipoVehiculo()
             {
                 Id = Constants.TIPO_VEHICULO_RESIDENTE,
                 Nombre = "Vehículo Residente",
                 PrecioMinuto = 0.05,
                 Vehiculos = [],
                 EsExterno = false,
             },
             new TipoVehiculo()
             {
                 Id = Constants.TIPO_VEHICULO_NO_RESIDENTE,
                 Nombre = "Vehículo No residente",
                 PrecioMinuto = 0.5,
                 Vehiculos = [],
                 EsExterno = true,
             });


        return modelBuilder;
    }
}
