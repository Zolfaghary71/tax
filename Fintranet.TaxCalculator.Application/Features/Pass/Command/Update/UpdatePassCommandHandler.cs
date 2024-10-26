using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Update
{
    public class UpdatePassCommandHandler : IRequestHandler<UpdatePassCommand, Domain.Entities.Pass>
    {
        private readonly IPassRepository _passRepository;

        public UpdatePassCommandHandler(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public async Task<Domain.Entities.Pass> Handle(UpdatePassCommand request, CancellationToken cancellationToken)
        {
            var pass = await _passRepository.GetByIdAsync(request.Id);
            if (pass == null)
            {
                return null;
            }

            pass.PassDateTime = request.PassTime;
            pass.Vehicle = await _passRepository.GetVehicleByIdAsync(request.VehicleId);
            pass.City = request.City;

            await _passRepository.UpdateAsync(pass);
            return pass;
        }
    }
}