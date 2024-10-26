using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Create
{
    public class CreatePassCommandHandler : IRequestHandler<CreatePassCommand, Domain.Entities.Pass>
    {
        private readonly IPassRepository _passRepository;

        public CreatePassCommandHandler(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public async Task<Domain.Entities.Pass> Handle(CreatePassCommand request, CancellationToken cancellationToken)
        {
            var pass = new Domain.Entities.Pass
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