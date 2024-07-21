using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Mvc;
using SuperAparcamiento.Functions.Extensions;
using SuperAparcamiento.Logic.Services.Interfaces;

namespace SuperAparcamiento.Functions.Functions;

/// <summary>
/// Funciones relacionadas a la generación de informes
/// </summary>
public class InformeFunctions(IInformeService informeService)
{
    /// <summary>
    /// Genera un informe con los pagos de los residentes
    /// </summary>
    [OpenApiOperation(operationId: nameof(InformePagoResidentes), nameof(InformeFunctions))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", bodyType: typeof(FileResult), Description = "El informe de residentes con los tiempos acumulados y el precio a pagar." )]
    [Function(nameof(InformePagoResidentes))]
    public async Task<IActionResult> InformePagoResidentes(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = nameof(InformePagoResidentes))] HttpRequest req)
    {
        var excelFile = await informeService.GenerarInformeEstancias(); 

        return excelFile.DeliverToHttpResponse($"informe pagos de residentes {DateTime.Now:yyyyMMdd}.xlsx");
    }
}
