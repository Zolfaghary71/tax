using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Create
{
    public class CreatePassCommandHandler : IRequestHandler<CreatePassCommand, Pass>
    {
        private readonly IPassRepository _passRepository;
        private readonly IValidator<CreatePassCommand> _validator;

        public CreatePassCommandHandler(IPassRepository passRepository, IValidator<CreatePassCommand> validator)
        {
            _passRepository = passRepository;
            _validator = validator;
        }

        public async Task<Pass> Handle(CreatePassCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var pass = new Pass
            {
                PassDateTime = request.PassTime,
                Vehicle = await _passRepository.GetVehicleByIdAsync(request.VehicleId),
                City = request.City
            };

            await _passRepository.AddAsync(pass);
            return pass;
        }
    }
}