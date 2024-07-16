namespace SuperAparcamiento.Logic.Contract.Vehiculo;

/// <summary>
/// Contrato de solicitud para crear un vehículo
/// </summary>
public class CreateVehiculoContract
{
    /// <summary>
    /// Matrícula del vehículo
    /// </summary>
    public required string NumeroPlaca { get; set; }
}
