using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Queries.GetById
{
    public class GetPassByIdQueryHandler : IRequestHandler<GetPassByIdQuery, Domain.Entities.Pass>
    {
        private readonly IPassRepository _passRepository;

        public GetPassByIdQueryHandler(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public async Task<Domain.Entities.Pass> Handle(GetPassByIdQuery request, CancellationToken cancellationToken)
        {
            return await _passRepository.GetByIdAsync(request.Id);
        }
    }
}