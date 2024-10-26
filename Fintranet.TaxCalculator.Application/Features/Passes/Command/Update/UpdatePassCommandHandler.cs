using Fintranet.TaxCalculator.Application.Features.Passes.Command.Update;
using FluentValidation;
using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Update
{
    public class UpdatePassCommandHandler : IRequestHandler<UpdatePassCommand, Pass>
    {
        private readonly IPassRepository _passRepository;
        private readonly IValidator<UpdatePassCommand> _validator;

        public UpdatePassCommandHandler(IPassRepository passRepository, IValidator<UpdatePassCommand> validator)
        {
            _passRepository = passRepository;
            _validator = validator;
        }

        public async Task<Pass> Handle(UpdatePassCommand request, CancellationToken cancellationToken)
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

            pass.PassDateTime = request.PassTime;
            pass.Vehicle = await _passRepository.GetVehicleByIdAsync(request.VehicleId);
            pass.City = request.City;

            await _passRepository.UpdateAsync(pass);
            return pass;
        }
    }
}