﻿using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

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

        public  IEnumerable<Pass> CalculateTaxAsync(IEnumerable<Pass> passes)
        {
            var passesByDay = passes.GroupBy(p => p.PassDateTime.Date);
            var taxedPasses = new List<Pass>();

            foreach (var dayPasses in passesByDay)
            {
                taxedPasses.AddRange( CalculateDailyTax(dayPasses.ToList()));
            }

            return taxedPasses;
        }

        public IEnumerable<Pass> CalculateDailyTax(List<Pass> passes)
        {
            var sortedPasses = passes.OrderBy(p => p.PassDateTime).ToList();
            var firstPassDate = sortedPasses.First().PassDateTime.Date;
            var firstVehicle = sortedPasses.First().Vehicle;
            var firstCity = sortedPasses.First().City;
            
            if (sortedPasses.Any(p => p.City != firstCity))
            {
                throw new InvalidOperationException("All passes must be from the same city.");
            }
            if (sortedPasses.Any(p => p.PassDateTime.Date != firstPassDate))
            {
                throw new InvalidOperationException("All passes must be on the same day.");
            }

            if (sortedPasses.Any(p => p.Vehicle.VehicleType != firstVehicle.VehicleType))
            {
                throw new InvalidOperationException("All passes must belong to the same vehicle.");
            }

            if (IsExemptDay(firstPassDate))
            {
                foreach (var pass in sortedPasses)
                {
                    pass.SupposedTax = 0;
                    pass.IsTaxCalculated = true;
                }

                return sortedPasses;
            }

            if (IsTaxExempt(firstVehicle))
            {
                foreach (var pass in sortedPasses)
                {
                    pass.SupposedTax = 0;
                    pass.IsTaxCalculated = true;
                }

                return sortedPasses;
            }

            return CalculateTax(sortedPasses);
        }

        private IEnumerable<Pass> CalculateTax(List<Pass> sortedPasses)
        {
            decimal dailyTotal = 0;
            decimal dailyMax = 60;

            foreach (var pass in sortedPasses)
            {
                pass.SupposedTax = CalculateSupposedTax(pass.PassDateTime.TimeOfDay);
                pass.IsTaxCalculated = true;
                pass.ActualTax = 0;
            }

            Pass lastHighest = null;
            foreach (var pass in sortedPasses)
            {
                if (lastHighest == null || lastHighest.PassDateTime.AddMinutes(60) < pass.PassDateTime)
                {
                    pass.ActualTax = CalculateHighestTaxLast60Minutes(pass, sortedPasses);
                    pass.IsTheHighestTax = true;

                    var dailySum = sortedPasses.Where(p => p.IsTheHighestTax).Select(p => p.ActualTax).Sum();
                    if (dailySum >= dailyMax)
                    {
                        if (dailySum > dailyMax)
                            pass.ActualTax -= dailySum - dailyMax;
                        break;
                    }
                    lastHighest = pass;
                    continue;
                }

                lastHighest.ActualTax = CalculateHighestTaxLast60Minutes(pass, sortedPasses);
                pass.IsTheHighestTax = false; 
                
                dailyTotal = sortedPasses.Where(p => p.IsTheHighestTax).Select(p => p.ActualTax).Sum();
                if (dailyTotal >= dailyMax)
                {
                    if (dailyTotal > dailyMax)
                        pass.ActualTax -= dailyTotal - dailyMax;
                    break;
                }
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

        private decimal CalculateSupposedTax(TimeSpan timeOfDay)
        {
            var rule = _taxRules.FirstOrDefault(r => r.StartTime <= timeOfDay && r.EndTime >= timeOfDay);
            return rule?.Amount ?? 0;
        }

        private decimal CalculateHighestTaxLast60Minutes(Pass currentPass, List<Pass> sortedPasses)
        {
            var oneHourAgo = currentPass.PassDateTime.AddMinutes(-60);

            var recentPasses = sortedPasses
                .Where(p => p.PassDateTime >= oneHourAgo && p.PassDateTime <= currentPass.PassDateTime)
                .Select(p => p.SupposedTax ?? 0);

            return recentPasses.Any() ? recentPasses.Max() : 0;
        }

        private bool IsExemptDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            if (date.Month == 7)
            {
                return true;
            }

            var publicHolidays = new List<DateTime>
            {
                new DateTime(2013, 1, 1),
                new DateTime(2013, 3, 29),
                new DateTime(2013, 4, 1),
                new DateTime(2013, 5, 1),
                new DateTime(2013, 5, 9),
                new DateTime(2013, 6, 6),
                new DateTime(2013, 6, 21),
                new DateTime(2013, 12, 24),
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2013, 12, 31)
            };

            if (publicHolidays.Contains(date))
            {
                return true;
            }

            return false;
        }
    }
}