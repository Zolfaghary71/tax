using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implementations
{
    public class ExternalTaxCalculationStrategy : ICongestionTaxStratgy
    {
        public City City { get; }

        private readonly IExternalTaxService _externalTaxService;

        public ExternalTaxCalculationStrategy(IExternalTaxService externalTaxService, City city)
        {
            _externalTaxService = externalTaxService;
            City = city;
        }

        public Task<IEnumerable<Pass>> CalculateDailyTaxAsync(List<Pass> passes)
        {
            throw new NotImplementedException();
        }
    }
}