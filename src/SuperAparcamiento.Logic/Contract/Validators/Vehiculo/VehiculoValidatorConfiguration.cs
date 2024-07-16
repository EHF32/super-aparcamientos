namespace SuperAparcamiento.Logic.Contract.Validators;

public static class VehiculoValidatorConfiguration
{
    /// <summary>
    /// Máximo de caracteres para el número de placa
    /// </summary>
    public const int MaxNumeroPlacaLength = 10;

    /// <summary>
    /// Permitimos solo letras, numeros y guiones/espacios
    /// </summary>
    public const string NumeroPlacaRegex = @"^[a-zA-Z0-9\s-]*$";
}

