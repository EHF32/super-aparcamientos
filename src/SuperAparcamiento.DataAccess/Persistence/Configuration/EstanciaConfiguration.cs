using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Persistence.Configuration;

/// <summary>
/// Configuración de la entidad Estancia
/// </summary>
public class EstanciaConfiguration : IEntityTypeConfiguration<Estancia>
{
    public void Configure(EntityTypeBuilder<Estancia> builder)
    {
        builder.Property(e => e.FechaEntrada).IsRequired();
        builder.Property(e => e.FechaSalida).IsRequired(false);

        builder.Property(e => e.VehiculoId).IsRequired();

        // Relación entre Estancia y Vehículo
        builder.HasOne(e => e.Vehiculo)
            .WithMany(v => v.Estancias)
            .HasForeignKey(e => e.VehiculoId)
            .HasPrincipalKey(v => v.Id);
    }

}
