using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperAparcamiento.DataAccess.Repositories.Interfaces;
using SuperAparcamiento.Logic.Contract.Vehiculo;
using SuperAparcamiento.Logic.Services.Interfaces;
using SuperAparcamiento.Model.Common;
using SuperAparcamiento.Model.Entities;
using SuperAparcamiento.Model.Exceptions;

namespace SuperAparcamiento.Logic.Services;

/// <summary>
/// Servicio para la gestión de Vehículos
/// </summary>
public class VehiculoService(IVehiculoRepository vehiculoRepository, IMapper mapper) : IVehiculoService
{
    /// <inheritdoc/>
    public async Task<VehiculoResponseContract> CreateVehiculoOficial(CreateVehiculoContract vehiculoRequest)
    {
        return await CreateVehiculo(vehiculoRequest, Constants.TIPO_VEHICULO_OFICIAL);
    }

    /// <inheritdoc/>
    public async Task<VehiculoResponseContract> CreateVehiculoResidente(CreateVehiculoContract vehiculoRequest)
    {
        return await CreateVehiculo(vehiculoRequest, Constants.TIPO_VEHICULO_RESIDENTE);
    }

    /// <inheritdoc/>
    public async Task<VehiculoResponseContract> CreateVehiculoNoResidente(CreateVehiculoContract vehiculoRequest)
    {
        return await CreateVehiculo(vehiculoRequest, Constants.TIPO_VEHICULO_NO_RESIDENTE);
    }

    /// <inheritdoc/>
    public async Task<VehiculoResponseContract> DeleteVehiculo(string numeroPlaca)
    {
        var vehiculoToDelete = await vehiculoRepository.GetFirstAsync(v => v.NumeroPlaca == numeroPlaca);

        if (vehiculoToDelete == null)
        {
            throw new ResourceNotFoundException($"No se ha encontrado ningún vehículo con la placa {numeroPlaca}");
        }

        await vehiculoRepository.DeleteAsync(vehiculoToDelete);

        return mapper.Map<VehiculoResponseContract>(vehiculoToDelete);
    }

    /// <inheritdoc/>
    public async Task<VehiculoResponseContract?> GetVehiculoByNumeroDePlaca(string numeroPlaca)
    {
        var vehiculo = await vehiculoRepository.GetFirstAsync(v => v.NumeroPlaca == numeroPlaca);

        return mapper.Map<VehiculoResponseContract?>(vehiculo);
    }

    /// <summary>
    /// Crea un vehículo por el tipo especificado
    /// </summary>
    /// <param name="vehiculoRequest">Vehículo a crear</param>
    /// <param name="tipoVehiculoId">tipo de vehículo</param>
    /// <returns>Vehículo creado</returns>
    private async Task<VehiculoResponseContract> CreateVehiculo(CreateVehiculoContract vehiculoRequest, Guid tipoVehiculoId)
    {
        ArgumentNullException.ThrowIfNull(vehiculoRequest);
        ArgumentNullException.ThrowIfNull(tipoVehiculoId);

        var existeVehiculo = await vehiculoRepository.GetFirstAsync(v => v.NumeroPlaca == vehiculoRequest.NumeroPlaca);

        if (existeVehiculo != null)
        {
            throw new BadRequestException($"Ya existe un vehículo registrado con la placa {vehiculoRequest.NumeroPlaca}.");
        }

        var vehiculo = mapper.Map<Vehiculo>(vehiculoRequest);
        vehiculo.TipoVehiculoId = tipoVehiculoId;

        vehiculo = await vehiculoRepository.AddAsync(vehiculo);

        var result = await vehiculoRepository.GetFirstAsync(v => v.Id == vehiculo.Id, v => v.Include(i => i.TipoVehiculo));
        return mapper.Map<VehiculoResponseContract>(result);
    }
}
