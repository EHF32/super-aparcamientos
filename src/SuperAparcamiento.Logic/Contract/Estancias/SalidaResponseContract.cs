namespace SuperAparcamiento.Logic.Contract.Estancias;

public class SalidaResponseContract
{
    /// <summary>
    /// Hora de entrada del vehículo
    /// </summary>
    public required DateTime FechaEntrada { get; set; }

    /// <summary>
    /// Hora de salida del vehículo
    /// </summary>
    public required DateTime FechaSalida { get; set; }

    /// <summary>
    /// Vehículo que ha estacionado
    /// </summary>
    public required string NumeroPlaca { get; set; }

    /// <summary>
    /// CantidadPagar de la estancia
    /// </summary>
    public required double CantidadPagar { get; set; }

    /// <summary>
    /// Tiempo de estancia
    /// </summary>
    public required double TiempoEstancia { get; set; }
}
