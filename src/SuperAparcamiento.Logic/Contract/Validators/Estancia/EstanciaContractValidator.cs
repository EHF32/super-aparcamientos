using FluentValidation;
using SuperAparcamiento.Logic.Contract.Estancias;

namespace SuperAparcamiento.Logic.Contract.Validators;

public class EstanciaContractValidator : AbstractValidator<RegistrarEntradaSalidaContract>
{
    public EstanciaContractValidator()
    {
        var mensajeNoValido = "El número de placa introducido no es válido.";

        RuleFor(RuleFor => RuleFor.VehiculoNumeroPlaca)
            .NotEmpty()
                .WithMessage(mensajeNoValido)
            .MaximumLength(VehiculoValidatorConfiguration.MaxNumeroPlacaLength)
                .WithMessage(mensajeNoValido)
            .Matches(VehiculoValidatorConfiguration.NumeroPlacaRegex)
                .WithMessage(mensajeNoValido);
    }
}
