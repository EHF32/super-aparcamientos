﻿using Microsoft.Azure.Functions.Worker;
using System.Net;

namespace SuperAparcamiento.Functions.Extensions;

public static class FunctionContextExtensions
{
    /// <summary>
    /// Extensión para establecer el código de estado de la respuesta HTTP
    /// </summary>
    public static void SetHttpResponseStatusCode(
        this FunctionContext context,
        HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(context.GetHttpContext());

        var httpContext = context.GetHttpContext();

        if (httpContext != null)
        {
            httpContext.Response.StatusCode = (int)statusCode;
        }

        var response = context.GetHttpResponseData();

        if (response != null)
        {
            response.StatusCode = statusCode;
        }
    }
}

