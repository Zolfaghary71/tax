using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implementations
{
    public class TaxCalculationService
    {
        private readonly ICongestionTaxStrategyFactory _strategyFactory;

        public TaxCalculationService(ICongestionTaxStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        public async Task<IEnumerable<Pass>>  CalculateTax(IEnumerable<Pass> passes)
        {
            ValidatePasses(passes);
            
            if (!passes.Any())
            {
                throw new ArgumentNullException("Passes collection is empty.");
            }

            var city = passes.First().City;
            var strategy = _strategyFactory.GetStrategy(city);

            return await strategy.CalculateDailyTaxAsync(passes.ToList());
        }
        private void ValidatePasses(IEnumerable<Pass> passes)
        {
            var firstPass = passes.First();
            var city = firstPass.City;
            var vehicle = firstPass.Vehicle;
            var year = firstPass.PassTime.Year;

            foreach (var pass in passes)
            {
                if (pass.City != city || pass.Vehicle != vehicle || pass.PassTime.Year != year)
                {
                    throw new InvalidOperationException("All passes must have the same city, vehicle, and year.");
                }
            }
        }
    }
}