using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using SuperAparcamiento.Functions.Extensions;
using SuperAparcamiento.Model.Exceptions;
using System.Net;

namespace SuperAparcamiento.Functions.Middleware;

/// <summary>
/// Middleware para capturar errores y devolver una respuesta estandarizada
/// </summary>
public class SuperAparcamientoErrorMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(
        FunctionContext context,
        FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            //crea un logger
            var logger = context.GetLogger<SuperAparcamientoErrorMiddleware>();
            logger.LogError(ex, "Error en la función");

            var httpResponse = context.GetHttpContext()!.Response;

            // Por defecto se devuelve un error 500, error interno del servidor
            var statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = "Error interno del servidor";

            // Si es de tipo SuperAparcamientoException, se devuelve directamente el error.
            if (ex is SuperAparcamientoException aparcamientoException)
            {
                statusCode = (HttpStatusCode)aparcamientoException.StatusCode;
                errorMessage = aparcamientoException.Message;
            }

            // Si es de tipo ValidationException, se devuelve un error 400 con los mensajes de validación
            if (ex is ValidationException validationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = string.Join(", ", validationException.Errors);
            }

            // Si es de tipo InvalidOperationException, se devuelve un error 400.
            if (ex is InvalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = "Operación no válida";
            }

            context.SetHttpResponseStatusCode(statusCode);
            await httpResponse.WriteAsJsonAsync(new { error = errorMessage, code = statusCode });

            return;
        }
    }
}
