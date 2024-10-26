using FluentValidation;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Delete
{
    public class DeletePassCommandValidator : AbstractValidator<DeletePassCommand>
    {
        public DeletePassCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}