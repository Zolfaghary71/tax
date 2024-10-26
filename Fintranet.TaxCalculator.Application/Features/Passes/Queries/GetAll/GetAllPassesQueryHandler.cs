using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Queries.GetAll
{
    public class GetAllPassesQueryHandler : IRequestHandler<GetAllPassesQuery, List<PassVm>>
    {
        private readonly IPassRepository _passRepository;

        public GetAllPassesQueryHandler(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }

        public async Task<List<PassVm>> Handle(GetAllPassesQuery request, CancellationToken cancellationToken)
        {
            var passes = await _passRepository.GetAllAsync();
            return passes.Select(p => new PassVm
            {
                Id = p.Id,
                Tax = p.ActualTax,
                DatePassed = p.PassDateTime
            }).ToList();
        }
    }
}