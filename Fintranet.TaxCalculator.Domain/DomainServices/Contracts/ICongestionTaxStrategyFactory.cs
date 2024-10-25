using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ICongestionTaxStrategyFactory
    {
        ICongestionTaxStratgy GetStrategy(City city);
    }
}