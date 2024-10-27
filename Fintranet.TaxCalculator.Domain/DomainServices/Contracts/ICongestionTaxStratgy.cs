using Fintranet.TaxCalculator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ICongestionTaxStratgy
    {
        public IEnumerable<Pass> CalculateDailyTax(List<Pass> passes);

    }
}