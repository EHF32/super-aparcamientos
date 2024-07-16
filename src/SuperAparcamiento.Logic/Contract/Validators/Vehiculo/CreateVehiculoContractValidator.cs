using FluentValidation;
using SuperAparcamiento.Logic.Contract.Vehiculo;

namespace SuperAparcamiento.Logic.Contract.Validators;

public class CreateVehiculoContractValidator : AbstractValidator<CreateVehiculoContract>
{
    public CreateVehiculoContractValidator()
    {
        RuleFor(RuleFor => RuleFor.NumeroPlaca)
            .NotEmpty()
                .WithMessage("El número de placa no puede estar vacía")
            .MaximumLength(VehiculoValidatorConfiguration.MaxNumeroPlacaLength)
                .WithMessage("El número de placa no puede tener más de 10 caracteres")
            .Matches(VehiculoValidatorConfiguration.NumeroPlacaRegex)
                .WithMessage("El número de placa tiene caracteres no válidos.");
    }
}
