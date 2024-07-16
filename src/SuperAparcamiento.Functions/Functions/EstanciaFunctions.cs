using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
using Microsoft.Extensions.DependencyInjection;
using SuperAparcamiento.Logic.Services;
using FluentValidation;
using SuperAparcamiento.Logic.Contract.Estancias;

namespace SuperAparcamiento.Functions.Functions;

/// <summary>
/// Funciones relacionadas con las estancias
/// </summary>
public class EstanciaFunctions(ILogger<EstanciaFunctions> logger, IEstanciaService estanciaService, IServiceProvider serviceProvider)
{

    /// <summary>
    /// Registra la entrada de un vehículo en el aparcamiento
    /// </summary>
    [OpenApiOperation(operationId: nameof(RegistrarEntrada), nameof(EstanciaFunctions))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(RegistrarEntradaSalidaContract), Required = true, Description = "Registra la entrada de un vehículo en el aparcamiento")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json ", bodyType: typeof(RegistrarEntradaResponseContract), Description = "El registro de la entrada del vehículo")]
    [Function(nameof(RegistrarEntrada))]
    public async Task<IActionResult> RegistrarEntrada(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = nameof(RegistrarEntrada))] HttpRequest req, [FromBody] RegistrarEntradaSalidaContract request)
    {
        await serviceProvider.GetService<IValidator<RegistrarEntradaSalidaContract>>().ValidateAndThrowAsync(request); 

        logger.LogDebug("Registrando entrada del vehículo {Placa}", request.VehiculoNumeroPlaca);

        var result = await estanciaService.RegistrarEntrada(request);

        return new OkObjectResult(result);
    }

    /// <summary>
    /// Registra la salida de un vehículo en el aparcamiento
    /// <returns></returns>
    [OpenApiOperation(operationId: nameof(RegistrarSalida), nameof(EstanciaFunctions))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(RegistrarEntradaSalidaContract), Required = true, Description = "Registra la salida de un vehículo en el aparcamiento")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json ", bodyType: typeof(SalidaResponseContract), Description = "El registro de la salida del vehículo")]
    [Function(nameof(RegistrarSalida))]
    public async Task<IActionResult> RegistrarSalida(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = nameof(RegistrarSalida))] HttpRequest req, [FromBody] RegistrarEntradaSalidaContract request)
    {
        await serviceProvider.GetService<IValidator<RegistrarEntradaSalidaContract>>().ValidateAndThrowAsync(request);

        logger.LogDebug("Registrando salida del vehículo {Placa}", request.VehiculoNumeroPlaca);

        var result = await estanciaService.RegistrarSalida(request);

        return new OkObjectResult(result);
    }

    /// <summary>
    /// Comienza un nuevo mes, eliminando las estancias registradas y poniendo
    /// a cero el tiempo estacionado por los vehículos de residentes
    /// </summary>
    [OpenApiOperation(operationId: nameof(ComienzaMes), nameof(EstanciaFunctions))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "La aplicación elimina las estancias registradas y pone a cero el tiempo estacionado por los vehículos de residentes.")]
    [Function(nameof(ComienzaMes))]
    public async Task<IActionResult> ComienzaMes(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = nameof(ComienzaMes))] HttpRequest req)
    {

        logger.LogDebug("Comenzando nuevo mes");

        await estanciaService.ComienzaMes();

        return new OkResult();
    }



}