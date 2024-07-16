using Microsoft.EntityFrameworkCore;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Persistence.Configuration;

/// <summary>
/// Configuración de la entidad Vehículo
/// </summary>
public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vehiculo> builder)
    {
        builder.Property(v => v.NumeroPlaca).IsRequired().HasMaxLength(10);
        builder.HasIndex(v => v.NumeroPlaca).IsUnique();

        builder.Property(v => v.TipoVehiculoId).IsRequired();

        // Relación entre Vehículo y sus Tipos
        builder.HasOne(v => v.TipoVehiculo)
            .WithMany(t => t.Vehiculos)
            .HasForeignKey(v => v.TipoVehiculoId)
            .HasPrincipalKey(e => e.Id);
    }

}
