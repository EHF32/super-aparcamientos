using SuperAparcamiento.Model.Common;

namespace SuperAparcamiento.Model.Entities;

/// <summary>
/// Clase que representa una estancia de un vehículo en el aparcamiento
/// </summary>
public class Estancia : BaseEntity
{
    /// <summary>
    /// Hora de entrada del vehículo
    /// </summary>
    public DateTime FechaEntrada { get; set; }

    /// <summary>
    /// Hora de salida del vehículo
    /// </summary>
    public DateTime? FechaSalida { get; set; }

    /// <summary>
    /// Identificador del vehículo
    /// </summary>
    public Guid VehiculoId { get; set; }

    /// <summary>
    /// Vehículo que ha estacionado
    /// </summary>
    public Vehiculo Vehiculo { get; set; } = default!;
}
