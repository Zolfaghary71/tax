using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintranet.TaxCalculator.Application.Services
{
    public class TaxCalculationService
    {
        private readonly Dictionary<City, ICongestionTaxStratgy> _strategies;

        public TaxCalculationService(IEnumerable<ICongestionTaxStratgy> strategies)
        {
            _strategies = strategies.ToDictionary(strategy => strategy.City);
        }

        public IEnumerable<Pass> CalculateTax(IEnumerable<Pass> passes)
        {
            if (!passes.Any())
            {
                throw new ArgumentException("Passes collection is empty.");
            }

            var city = passes.First().City;
            if (!_strategies.TryGetValue(city, out var strategy))
            {
                throw new InvalidOperationException($"No strategy found for city: {city}");
            }

            return strategy.CalculateTax(passes);
        }
    }
}