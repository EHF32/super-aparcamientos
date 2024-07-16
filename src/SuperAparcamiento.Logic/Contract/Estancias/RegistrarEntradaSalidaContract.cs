namespace SuperAparcamiento.Logic.Contract.Estancias;

/// <summary>
/// Contrato de solicitud para Registro de entradas/salidas del estacionamiento
/// </summary>
public class RegistrarEntradaSalidaContract
{
    /// <summary>
    /// Matrícula del vehículo a registrar una entrada o salida del estacionamiento.
    /// </summary>
    public required string VehiculoNumeroPlaca { get; set; }
}

