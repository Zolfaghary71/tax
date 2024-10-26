using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using FluentValidation;
using MediatR;
using Pass = Fintranet.TaxCalculator.Domain.Entities.Pass;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Delete
{
    public class DeletePassCommandHandler : IRequestHandler<DeletePassCommand, Pass>
    {
        private readonly IPassRepository _passRepository;
        private readonly IValidator<DeletePassCommand> _validator;

        public DeletePassCommandHandler(IPassRepository passRepository, IValidator<DeletePassCommand> validator)
        {
            _passRepository = passRepository;
            _validator = validator;
        }

        public async Task<Pass> Handle(DeletePassCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var pass = await _passRepository.GetByIdAsync(request.Id);
            if (pass == null)
            {
                throw new KeyNotFoundException($"Pass with ID {request.Id} was not found.");
            }

            await _passRepository.DeleteAsync(pass);
            return pass;
        }
    }
}