using System.Net;

namespace SuperAparcamiento.Model.Exceptions;

/// <summary>
/// Excepcion base para la gestión centralizada de errores
/// </summary>
[Serializable]
public abstract class SuperAparcamientoException : Exception
{
    private readonly int httpStatusCode;

    public SuperAparcamientoException(int httpStatusCode)
    {
        this.httpStatusCode = httpStatusCode;
    }

    public SuperAparcamientoException(HttpStatusCode httpStatusCode)
    {
        this.httpStatusCode = (int)httpStatusCode;
    }

    public SuperAparcamientoException(int httpStatusCode, string message) : base(message)
    {
        this.httpStatusCode = httpStatusCode;
    }

    public SuperAparcamientoException(HttpStatusCode httpStatusCode, string message) : base(message)
    {
        this.httpStatusCode = (int)httpStatusCode;
    }

    public SuperAparcamientoException(int httpStatusCode, string message, Exception inner) : base(message, inner)
    {
        this.httpStatusCode = httpStatusCode;
    }

    public SuperAparcamientoException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
    {
        this.httpStatusCode = (int)httpStatusCode;
    }

    public int StatusCode { get { return this.httpStatusCode; } }
}

