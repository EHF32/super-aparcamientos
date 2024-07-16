using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using SuperAparcamiento.DataAccess.Repositories.Interfaces;
using SuperAparcamiento.Model.Common;

namespace SuperAparcamiento.Logic.Services;

/// <summary>
/// Servicio para la generación de informes.
/// </summary>
public class InformeService(IVehiculoRepository vehiculoRepository, IEstanciaService estanciaService) : IInformeService
{
    /// <inheritdoc/>
    public async Task<XLWorkbook> GenerarInformeEstancias()
    {
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Estancias");
        int fila = 1;

        // Cabecera
        ws.Cell(fila, 1).Value = "Núm. de placa";
        ws.Cell(fila, 2).Value = "Tiempo de estancia (min.)";
        ws.Cell(fila, 3).Value = "Cantidad a pagar";
        fila++;

        // Obtenemos todos los vehículos residentes.
        var vehiculos = (await vehiculoRepository.GetAllAsync(v => v.TipoVehiculoId == Constants.TIPO_VEHICULO_RESIDENTE,
                                                              v => v.Include(i => i.Estancias).Include(i => i.TipoVehiculo))); 

        foreach (var vehiculo in vehiculos)
        {
            // Calcular el tiempo y el precio de cada estancia del vehículo
            var estancias = vehiculo.Estancias.Select(estanciaService.CalcularPrecioYTiempo);

            ws.Cell(fila, 1).Value = vehiculo.NumeroPlaca;
            ws.Cell(fila, 2).Value = estancias.Select(e => e.TiempoEstancia).Sum();
            ws.Cell(fila, 3).Value = estancias.Select(e => e.CantidadPagar).Sum();
            fila++;
        }

        // Ajustar formato
        ws.RangeUsed().CreateTable(); 
        ws.Columns().AdjustToContents();

        return wb;
    }
}
