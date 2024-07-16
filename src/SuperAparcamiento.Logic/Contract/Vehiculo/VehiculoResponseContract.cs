namespace SuperAparcamiento.Logic.Contract.Vehiculo;


/// <summary>
/// Contrato para la respuesta de un vehículo
/// </summary>
public class VehiculoResponseContract
{
    /// <summary>
    /// Identificador del vehículo
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Matrícula del vehículo
    /// </summary>
    public required string NumeroPlaca { get; set; }

    /// <summary>
    /// Identificador del Tipo de vehículo
    /// </summary>
    public Guid TipoVehiculoId { get; set; }

    /// <summary>
    /// Nombre del tipo de Veh    
    /// </summary>
    public required string TipoVehiculoNombre { get; set; }
}
