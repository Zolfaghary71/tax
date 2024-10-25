using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ICongestionTaxStratgy
    {
        City City { get; }
        IEnumerable<Pass> CalculateTax(IEnumerable<Pass> passes);
    }
}