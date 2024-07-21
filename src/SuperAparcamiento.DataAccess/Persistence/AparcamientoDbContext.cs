using Microsoft.EntityFrameworkCore;
using SuperAparcamiento.Model.Entities;
using System.Reflection;

namespace SuperAparcamiento.DataAccess.Persistence;


/// <summary>
/// Base de datos de la aplicaci�n de SuperAparcamiento
/// </summary>
public class AparcamientoDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Estancias de veh�culos en el aparcamiento
    /// </summary>
    public DbSet<Estancia> Estancias => Set<Estancia>();

    /// <summary>
    /// Tipos de veh�culos
    /// </summary>
    public DbSet<TipoVehiculo> TipoVehiculos => Set<TipoVehiculo>();

    /// <summary>
    /// Veh�culos
    /// </summary>
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica las configuraciones de las entidades
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Introduce los datos iniciales para los tipos de Veh�culos.  
        modelBuilder.SeedDatabase();
    }
}