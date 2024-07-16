using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.DataAccess.Persistence.Configuration;

/// <summary>
/// Configuración de la entidad TipoVehiculo
/// </summary>
public class TipoVehiculoConfiguration : IEntityTypeConfiguration<TipoVehiculo>
{
    public void Configure(EntityTypeBuilder<TipoVehiculo> builder)
    {
        builder.Property(t => t.Nombre).IsRequired().HasMaxLength(50);
        builder.HasIndex(t => t.Nombre).IsUnique();
    }

}
