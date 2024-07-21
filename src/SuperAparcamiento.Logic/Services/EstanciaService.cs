using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperAparcamiento.DataAccess.Repositories;
using SuperAparcamiento.Logic.Contract.Estancias;
using SuperAparcamiento.Logic.Contract.Vehiculo;
using SuperAparcamiento.Logic.Services.Interfaces;
using SuperAparcamiento.Model.Entities;
using SuperAparcamiento.Model.Exceptions;

namespace SuperAparcamiento.Logic.Services;

/// <summary>
/// Servicio para la gestión de Estancias
/// </summary>
public class EstanciaService(IEstanciaRepository estanciaRepository, IVehiculoService vehiculoService, IMapper mapper) : IEstanciaService
{
    /// <inheritdoc/>
    public async Task<RegistrarEntradaResponseContract> RegistrarEntrada(RegistrarEntradaSalidaContract estanciaContract)
    {
        ArgumentNullException.ThrowIfNull(estanciaContract);

        // Se comprueba si el vehículo ya está en el aparcamiento
        var vehiculo = await vehiculoService.GetVehiculoByNumeroDePlaca(estanciaContract.VehiculoNumeroPlaca);
        if (vehiculo != null)
        {
            // No se puede registrar la entrada
            var estanciaActual = await estanciaRepository.GetFirstAsync(e => e.VehiculoId == vehiculo.Id && e.FechaSalida == null);

            if (estanciaActual != null)
            {
                throw new BadRequestException($"El vehículo con matrícula {estanciaContract.VehiculoNumeroPlaca} ya se encuentra en el aparcamiento");
            }
        }
        else
        {
            // Para los vehículos no residentes, se crea un vehículo nuevo temporal
            vehiculo = await vehiculoService.CreateVehiculoNoResidente(new CreateVehiculoContract { NumeroPlaca = estanciaContract.VehiculoNumeroPlaca });
        }

        var result = await estanciaRepository.AddAsync(new Estancia()
        {
            VehiculoId = vehiculo.Id,
            FechaEntrada = DateTime.Now,
        });

        return mapper.Map<RegistrarEntradaResponseContract>(result);
    }

    /// <inheritdoc/>
    public async Task<SalidaResponseContract> RegistrarSalida(RegistrarEntradaSalidaContract request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var estancia = await estanciaRepository
                        .GetFirstAsync(e => e.Vehiculo.NumeroPlaca == request.VehiculoNumeroPlaca && e.FechaSalida == null,
                                       e => e.Include(i => i.Vehiculo).ThenInclude(i => i.TipoVehiculo)) 
            ?? throw new ResourceNotFoundException($"No se ha encontrado ninguna estancia activa para el vehículo con matrícula {request.VehiculoNumeroPlaca}");

        // Se actualiza la fecha de salida a la actual
        estancia.FechaSalida = DateTime.Now;
        await estanciaRepository.UpdateAsync(estancia);

        // Transformamos al modelo de contrato de salida con el precio y tiempo de la estancia.
        var resultado = CalcularPrecioYTiempo(estancia);

        // Si el vehículo es externo, se elimina el registro temporal de vehículo en los registros de la base de datos
        if (estancia.Vehiculo.TipoVehiculo.EsExterno)
        {
            await vehiculoService.DeleteVehiculo(request.VehiculoNumeroPlaca);
        }

        return resultado;
    }

    /// <inheritdoc/>
    public async Task ComienzaMes()
    {
        // Se eliminan las estancias de los vehículos marcados como no externos
        // (Residentes y oficiales), así estará a cero el tiempo estacionado ya
        // que se calcula en base a la fecha de entrada/salida.
        var result = await estanciaRepository.DeleteAsync(e => !e.Vehiculo.TipoVehiculo.EsExterno);

        if (result == 0)
        {
            throw new BadRequestException("No se ha realizado ningún cambio desde la última ejecución.");
        }
    }
    
    /// <inheritdoc/>
    public SalidaResponseContract CalcularPrecioYTiempo(Estancia estancia)
    {
        ArgumentNullException.ThrowIfNull(estancia);

        if (estancia.FechaSalida == null)
        {
            throw new InvalidOperationException($"La estancia no ha finalizado: {estancia.Id}");
        }

        if (estancia.Vehiculo?.TipoVehiculo == null)
        {
            throw new InvalidOperationException($"No se puede obtener el precio la estancia: {estancia.Id}");
        }

        // Se calcula el precio en base al tiempo de estancia y el precio por minuto para el tipo de vehículo
        var tiempoEstancia = estancia.FechaSalida.Value - estancia.FechaEntrada;
        var precio = tiempoEstancia.TotalMinutes * estancia.Vehiculo.TipoVehiculo.PrecioMinuto;

        // Se devuelve la información de la salida en formato de respuesta
        return new SalidaResponseContract
        {
            FechaEntrada = estancia.FechaEntrada,
            FechaSalida = estancia.FechaSalida.Value,
            CantidadPagar = Math.Round(precio, 2),
            TiempoEstancia = Math.Round(tiempoEstancia.TotalMinutes, 2),
            NumeroPlaca = estancia.Vehiculo.NumeroPlaca
        };
    }

}
