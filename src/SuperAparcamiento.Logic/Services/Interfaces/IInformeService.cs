using ClosedXML.Excel;

namespace SuperAparcamiento.Logic.Services.Interfaces;

public interface IInformeService
{
    /// <summary>
    /// Genera un archivo que detalla el tiempo estacionado y el dinero a
    /// pagar por cada uno de los vehículos de residentes.
    /// </summary>
    /// <returns></returns>
    Task<XLWorkbook> GenerarInformeEstancias();
}