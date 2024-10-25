using Fintranet.TaxCalculator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ICongestionTaxStratgy
    {
        public Task<IEnumerable<Pass>> CalculateDailyTaxAsync(List<Pass> passes);

    }
}