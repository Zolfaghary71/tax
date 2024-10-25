using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ITaxRuleRepository
    {
        Task<IEnumerable<TaxRule>> GetTaxRulesAsync(City city);
    }
}