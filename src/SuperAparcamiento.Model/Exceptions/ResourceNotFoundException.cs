using System.Net;

namespace SuperAparcamiento.Model.Exceptions;

/// <summary>
/// Excepción que se lanza cuando no se encuentra un recurso
/// </summary>

[Serializable]
public class ResourceNotFoundException : SuperAparcamientoException

{
    public ResourceNotFoundException(Type type) : base(HttpStatusCode.NotFound, $"No se ha encontrado el recurso de tipo {type.Name}") { }

    public ResourceNotFoundException() : base(HttpStatusCode.NotFound) { }

    public ResourceNotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }

    public ResourceNotFoundException(string message, Exception inner) : base(HttpStatusCode.NotFound, message, inner) { }
}