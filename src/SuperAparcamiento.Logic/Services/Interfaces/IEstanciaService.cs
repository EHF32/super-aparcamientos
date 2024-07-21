using SuperAparcamiento.Logic.Contract.Estancias;
using SuperAparcamiento.Model.Entities;

namespace SuperAparcamiento.Logic.Services.Interfaces;

public interface IEstanciaService
{
    /// <summary>
    /// Calcula el precio y el tiempo de una estancia y lo devuelve en un objeto de respuesta
    /// </summary>
    /// <param name="estancia">La estancia de la que se quiere calcular el precio y el tiempo</param>
    /// <returns>El objeto de contrato para la respuesta de la estancia con el precio y tiempo</returns>
    /// <exception cref="InvalidOperationException">Si la estancia no está finalizada o no se encuentra el tipo de vehículo.</exception> 
    SalidaResponseContract CalcularPrecioYTiempo(Estancia estancia);

    /// <summary>
    /// Comienza un nuevo mes eliminando todas las estancias registradas en los
    /// coches oficiales y pone a cero el tiempo estacionado por los vehículos
    /// de residentes.
    /// </summary>
    Task ComienzaMes();

    /// <summary>
    /// Crea un vehículo por el tipo especificado
    /// </summary>
    /// <param name="estanciaContract">Vehículo a registrar entrada</param>
    /// <returns>Estancia creada</returns>
    Task<RegistrarEntradaResponseContract> RegistrarEntrada(RegistrarEntradaSalidaContract estanciaContract);

    /// <summary>
    /// Registra la salida de un vehículo
    /// </summary>
    /// <param name="request">Vehículo a registrar la salida del aparcamiento</param>
    /// <returns>Vehículo creado</returns>
    Task<SalidaResponseContract> RegistrarSalida(RegistrarEntradaSalidaContract request);
}