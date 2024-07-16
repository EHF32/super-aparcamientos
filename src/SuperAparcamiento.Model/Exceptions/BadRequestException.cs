using System.Net;

namespace SuperAparcamiento.Model.Exceptions;

/// <summary>
/// Excepción que se lanza cuando no se encuentra un recurso
/// </summary> 
[Serializable]
public class BadRequestException : SuperAparcamientoException

{
    public BadRequestException() : base(HttpStatusCode.BadRequest) { }

    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }

    public BadRequestException(string message, Exception inner) : base(HttpStatusCode.BadRequest, message, inner) { }
}