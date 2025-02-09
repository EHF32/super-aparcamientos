using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
using Microsoft.Extensions.DependencyInjection;
using SuperAparcamiento.Logic.Contract.Vehiculo;
using FluentValidation;
using SuperAparcamiento.Logic.Services.Interfaces;

namespace SuperAparcamiento.Functions.Functions;

/// <summary>
/// Funciones relacionadas con los vehÝculos
/// </summary>
public class VehiculoFunctions(ILogger<VehiculoFunctions> logger, IVehiculoService vehiculoService, IServiceProvider serviceProvider)
{

    /// <summary>
    /// Da de alta un vehÝculo oficial
    /// </summary>
    [OpenApiOperation(operationId: nameof(CreateVehiculoOficial), nameof(VehiculoFunctions))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateVehiculoContract), Required = true, Description = "Da de alta un vehÝculo oficial")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json ", bodyType: typeof(VehiculoResponseContract), Description = "El vehÝculo oficial dado de alta")]
    [Function(nameof(CreateVehiculoOficial))]
    public async Task<IActionResult> CreateVehiculoOficial(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = nameof(CreateVehiculoOficial))] HttpRequest req, [FromBody] CreateVehiculoContract vehiculo)
    { 
        await serviceProvider.GetService<IValidator<CreateVehiculoContract>>().ValidateAndThrowAsync(vehiculo);

        logger.LogDebug("Creando vehiculo oficial {Placa}", vehiculo.NumeroPlaca);

        var result = await vehiculoService.CreateVehiculoOficial(vehiculo);

        return new OkObjectResult(result);
    }

    /// <summary>
    /// Da de alta un vehÝculo residente
    /// </summary>
    [OpenApiOperation(operationId: nameof(CreateVehiculoResidente), nameof(VehiculoFunctions))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateVehiculoContract), Required = true, Description = "Da de alta un vehÝculo de residente")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json ", bodyType: typeof(VehiculoResponseContract), Description = "El vehÝculo de residente dado de alta")]
    [Function(nameof(CreateVehiculoResidente))]
    public async Task<IActionResult> CreateVehiculoResidente(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = nameof(CreateVehiculoResidente))] HttpRequest req, [FromBody] CreateVehiculoContract vehiculo)
    {
        await serviceProvider.GetService<IValidator<CreateVehiculoContract>>().ValidateAndThrowAsync(vehiculo);

        logger.LogDebug("Creando vehiculo residente {Placa}", vehiculo.NumeroPlaca);

        var result = await vehiculoService.CreateVehiculoResidente(vehiculo);

        return new OkObjectResult(result);
    } 
}