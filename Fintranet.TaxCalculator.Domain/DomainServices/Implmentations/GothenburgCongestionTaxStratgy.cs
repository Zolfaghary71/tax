using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implmentations
{
    public class GothenburgCongestionTaxStratgy : ICongestionTaxStratgy
    {
        public City City => City.Gothenburg;

        private const decimal DailyMaxTax = 60m;
        private static readonly List<(TimeSpan StartTime, TimeSpan EndTime, decimal Amount)> TaxRules = new()
        {
            (new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0), 8),
            (new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0), 13),
            (new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0), 18),
            (new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0), 13),
            (new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0), 8),
            (new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0), 13),
            (new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 0), 18),
            (new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0), 13),
            (new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0), 8),
            (new TimeSpan(18, 30, 0), new TimeSpan(5, 59, 0), 0)
        };

        public IEnumerable<Pass> CalculateTax(IEnumerable<Pass> passes)
        {
            ValidatePasses(passes);

            var passesByDay = passes.GroupBy(p => p.PassTime.Date);
            var taxedPasses = new List<Pass>();

            foreach (var dayPasses in passesByDay)
            {
                taxedPasses.AddRange(CalculateDailyTax(dayPasses.ToList()));
            }

            return taxedPasses;
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

        private IEnumerable<Pass> CalculateDailyTax(List<Pass> passes)
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
            var rule = TaxRules.FirstOrDefault(r => r.StartTime <= timeOfDay && r.EndTime >= timeOfDay);
            return rule.Amount;
        }
    }
}