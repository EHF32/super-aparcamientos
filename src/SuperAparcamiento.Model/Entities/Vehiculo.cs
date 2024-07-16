using SuperAparcamiento.Model.Common;

namespace SuperAparcamiento.Model.Entities;

/// <summary>
/// Clase que representa un vehículo
/// </summary>
public class Vehiculo : BaseEntity
{
    /// <summary>
    /// Matrícula del vehículo
    /// </summary>
    public required string NumeroPlaca { get; set; }

    /// <summary>
    /// Identificador del Tipo de vehículo
    /// </summary>
    public Guid TipoVehiculoId { get; set; }

    /// <summary>
    /// Tipo de vehículo
    /// </summary>
    public TipoVehiculo TipoVehiculo { get; set; } = default!;

    /// <summary>
    /// Estancias del vehículo en el aparcamiento
    /// </summary>
    public ICollection<Estancia> Estancias { get; set; } = default!;
}
