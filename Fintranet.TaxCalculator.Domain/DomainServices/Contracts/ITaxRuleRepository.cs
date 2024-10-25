using Fintranet.TaxCalculator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fintranet.TaxCalculator.Domain.ValueObjects;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ITaxRuleRepository
    {
        Task<IEnumerable<TaxRule>> GetTaxRulesAsync(City city);
    }
}