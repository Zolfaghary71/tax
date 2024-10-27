using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implementations
{
    public class CongestionTaxStrategyFactory : ICongestionTaxStrategyFactory
    {
        private readonly Dictionary<City, Func<IServiceProvider, ICongestionTaxStratgy>> _strategyMap;
        private readonly IServiceProvider _serviceProvider;

        public CongestionTaxStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _strategyMap = new Dictionary<City, Func<IServiceProvider, ICongestionTaxStratgy>>
            {
                { City.Gothenburg, sp => new StandardCongestionTaxStratgy(City.Gothenburg, sp.GetRequiredService<ITaxRuleRepository>()) },
                { City.London, sp => new ExternalTaxCalculationStrategy(sp.GetRequiredService<IExternalTaxService>(), City.London) }
            };
        }

        public ICongestionTaxStratgy GetStrategy(City city)
        {
            if (!_strategyMap.TryGetValue(city, out var strategyFactory))
            {
                throw new InvalidOperationException($"No strategy found for city: {city}");
            }

            return strategyFactory(_serviceProvider);
        }
    }
}