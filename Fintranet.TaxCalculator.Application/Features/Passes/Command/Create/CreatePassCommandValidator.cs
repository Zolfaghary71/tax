using FluentValidation;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Create
{
    public class CreatePassCommandValidator : AbstractValidator<CreatePassCommand>
    {
        public CreatePassCommandValidator()
        {
            RuleFor(x => x.PassTime)
                .NotEmpty().WithMessage("PassTime is required.");

            RuleFor(x => x.VehicleId)
                .NotEmpty().WithMessage("VehicleId is required.");

            RuleFor(x => x.City)
                .IsInEnum().WithMessage("City must be a valid enum value.");
        }
    }
}