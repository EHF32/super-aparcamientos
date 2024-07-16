using SuperAparcamiento.Model.Common;

namespace SuperAparcamiento.Model.Entities;

/// <summary>
/// Clase que representa un tipo de vehículo
/// </summary>
public class TipoVehiculo : BaseEntity
{
    /// <summary>
    /// Nombre identificativo del tipo de vehículo
    /// </summary>
    public required string Nombre { get; set; }

    /// <summary>
    /// Precio por minuto de estacionamiento para el tipo de vehículo (en céntimos)
    /// </summary>
    public required double PrecioMinuto { get; set; }

    /// <summary>
    /// Vehículos de este tipo
    /// </summary>
    public ICollection<Vehiculo> Vehiculos { get; set; } = default!;

    /// <summary>
    /// Indica si el vehículo es de tipo externo al aparcamiento.
    /// Ejemplo: Los vehiculos oficiales y residentes son internos, los
    /// visitantes son externos, y la gestión de los mismos es diferente.
    /// </summary>
    public required bool EsExterno { get; set; }
}
