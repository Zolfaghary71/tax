using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Delete
{
    public class DeletePassCommandHandler : IRequestHandler<DeletePassCommand, bool>
    {
        private readonly IPassRepository _passRepository;

        public DeletePassCommandHandler(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public async Task<bool> Handle(DeletePassCommand request, CancellationToken cancellationToken)
        {
            var pass = await _passRepository.GetByIdAsync(request.Id);
            if (pass == null)
            {
                return false;
            }

            await _passRepository.DeleteAsync(pass);
            return true;
        }
    }
}