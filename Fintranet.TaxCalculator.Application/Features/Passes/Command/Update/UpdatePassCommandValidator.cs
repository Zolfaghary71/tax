using FluentValidation;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Update
{
    public class UpdatePassCommandValidator : AbstractValidator<UpdatePassCommand>
    {
        public UpdatePassCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.PassTime)
                .NotEmpty().WithMessage("PassTime is required.");

            RuleFor(x => x.VehicleId)
                .NotEmpty().WithMessage("VehicleId is required.");

            RuleFor(x => x.City)
                .IsInEnum().WithMessage("City must be a valid enum value.");
        }
    }
}