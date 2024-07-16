namespace SuperAparcamiento.Logic.Contract.Estancias;

public class RegistrarEntradaResponseContract
{
    /// <summary>
    /// Hora de entrada del vehículo
    /// </summary>
    public required DateTime FechaEntrada { get; set; }

    /// <summary>
    /// Identificador del vehículo
    /// </summary>
    public required Guid VehiculoId { get; set; }

    /// <summary>
    /// Identificador del vehículo
    /// </summary>
    public required string VehiculoNumeroPlaca { get; set; }
}
