using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implementations
{
    public class CongestionTaxStrategyFactory : ICongestionTaxStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<City, Type> _strategyMap;

        public CongestionTaxStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _strategyMap = new Dictionary<City, Type>
            {
                { City.Gothenburg, typeof(StandardCongestionTaxStratgy) },
            };
        }

        public ICongestionTaxStratgy GetStrategy(City city)
        {
            if (!_strategyMap.TryGetValue(city, out var strategyType))
            {
                throw new InvalidOperationException($"No strategy found for city: {city}");
            }

            return (ICongestionTaxStratgy)_serviceProvider.GetService(strategyType);
        }
    }
}