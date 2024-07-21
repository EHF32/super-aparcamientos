using SuperAparcamiento.Logic.Contract.Vehiculo;

namespace SuperAparcamiento.Logic.Services.Interfaces;

/// <summary>
/// Servicio para la gestión de vehículos
/// </summary>
public interface IVehiculoService
{
    /// <summary>
    /// Crea un vehículo oficial
    /// </summary>
    /// <param name="vehiculoRequest">El vehículo a crear</param>
    /// <returns>El vehículo oficial creado</returns>
    Task<VehiculoResponseContract> CreateVehiculoOficial(CreateVehiculoContract vehiculoRequest);

    /// <summary>
    /// Crea un vehículo de residente
    /// </summary>
    /// <param name="vehiculoRequest">El vehículo a crear</param>
    /// <returns>El vehículo oficial creado</returns>
    Task<VehiculoResponseContract> CreateVehiculoResidente(CreateVehiculoContract vehiculoRequest);

    /// <summary>
    /// Crea un vehículo no residente
    /// </summary>
    /// <param name="vehiculoRequest">El vehículo a crear</param>
    /// <returns>El vehículo oficial creado</returns>
    Task<VehiculoResponseContract> CreateVehiculoNoResidente(CreateVehiculoContract vehiculoRequest);

    /// <summary>
    /// Elimina un vehículo por su número de placa
    /// </summary>
    /// <param name="numeroPlaca">Numero de la placa a eliminar el vehículo</param>
    /// <returns>El vehículo eliminado</returns>
    Task<VehiculoResponseContract> DeleteVehiculo(string numeroPlaca);

    /// <summary>
    /// Obtiene un vehículo por su número de placa
    /// </summary>
    /// <param name="numeroPlaca">Numero de la placa a buscar el vehículo</param>
    /// <returns>El vehículo buscado o null</returns>
    Task<VehiculoResponseContract?> GetVehiculoByNumeroDePlaca(string numeroPlaca);
}