using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using Fintranet.TaxCalculator.Domain.ValueObjects;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implementations
{
    public class StandardCongestionTaxStratgy : ICongestionTaxStratgy
    {
        public City City { get; }

        private const decimal DailyMaxTax = 60m;
        private IEnumerable<TaxRule> _taxRules;

        public StandardCongestionTaxStratgy(City city, ITaxRuleRepository taxRuleRepository)
        {
            City = city;
            InitializeAsync(taxRuleRepository).Wait();
        }

        private async Task InitializeAsync(ITaxRuleRepository taxRuleRepository)
        {
            _taxRules = await taxRuleRepository.GetTaxRulesAsync(City);
        }

        public async Task<IEnumerable<Pass>> CalculateTaxAsync(IEnumerable<Pass> passes)
        {
            var passesByDay = passes.GroupBy(p => p.PassTime.Date);
            var taxedPasses = new List<Pass>();

            foreach (var dayPasses in passesByDay)
            {
                taxedPasses.AddRange(await CalculateDailyTaxAsync(dayPasses.ToList()));
            }

            return taxedPasses;
        }

        private async Task<IEnumerable<Pass>> CalculateDailyTaxAsync(List<Pass> passes)
        {
            var sortedPasses = passes.OrderBy(p => p.PassTime).ToList();
            decimal dailyTotal = 0;
            DateTime? lastPassTime = null;
            decimal highestTaxInPeriod = 0;

            foreach (var pass in sortedPasses)
            {
                if (IsTaxExempt(pass.Vehicle))
                {
                    pass.TaxAmount = 0;
                    pass.IsTaxCalculated = true;
                    continue;
                }

                var taxAmount = GetTaxAmount(pass.PassTime.TimeOfDay);
                if (lastPassTime.HasValue && (pass.PassTime - lastPassTime.Value).TotalMinutes <= 60)
                {
                    highestTaxInPeriod = Math.Max(highestTaxInPeriod, taxAmount);
                }
                else
                {
                    dailyTotal += highestTaxInPeriod;
                    highestTaxInPeriod = taxAmount;
                    lastPassTime = pass.PassTime;
                }

                pass.TaxAmount = taxAmount;
                pass.IsTaxCalculated = true;
            }

            dailyTotal += highestTaxInPeriod;
            dailyTotal = Math.Min(dailyTotal, DailyMaxTax);

            foreach (var pass in sortedPasses)
            {
                pass.TaxAmount = dailyTotal;
            }

            return sortedPasses;
        }

        private bool IsTaxExempt(Vehicle vehicle)
        {
            return vehicle.VehicleType == VehicleType.Bus ||
                   vehicle.VehicleType == VehicleType.EmergencyVehicle ||
                   vehicle.VehicleType == VehicleType.DiplomatVehicle ||
                   vehicle.VehicleType == VehicleType.MilitaryVehicle ||
                   vehicle.VehicleType == VehicleType.Motorcycle ||
                   vehicle.VehicleType == VehicleType.ForeignVehicle;
        }

        private decimal GetTaxAmount(TimeSpan timeOfDay)
        {
            var rule = _taxRules.FirstOrDefault(r => r.StartTime <= timeOfDay && r.EndTime >= timeOfDay);
            return rule?.Amount ?? 0;
        }
    }
}