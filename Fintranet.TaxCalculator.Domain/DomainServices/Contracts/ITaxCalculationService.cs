using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts;

public interface ITaxCalculationService
{
    public IEnumerable<Pass> CalculateTax(IEnumerable<Pass> passes);
}