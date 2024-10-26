using MediatR;
using Fintranet.TaxCalculator.Application.Features.Pass.Queries;
using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Handlers
{
    public class GetAllPassesQueryHandler : IRequestHandler<GetAllPassesQuery, List<PassDto>>
    {
        private readonly IPassRepository _passRepository;

        public GetAllPassesQueryHandler(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public async Task<List<PassDto>> Handle(GetAllPassesQuery request, CancellationToken cancellationToken)
        {
            var passes = await _passRepository.GetAllAsync();
            return passes.Select(p => new PassDto
            {
                Id = p.Id,
                Tax = p.ActualTax,
                DatePassed = p.PassDateTime
            }).ToList();
        }
    }
}