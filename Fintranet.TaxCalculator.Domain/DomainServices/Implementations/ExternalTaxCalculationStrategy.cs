using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Implementations
{
    public class ExternalTaxCalculationStrategy : ICongestionTaxStratgy
    {
        public City City { get; }

        private readonly IExternalTaxService _externalTaxService;

        public ExternalTaxCalculationStrategy(IExternalTaxService externalTaxService, City city)
        {
            _externalTaxService = externalTaxService;
            City = city;
        }

        public IEnumerable<Pass> CalculateTax(IEnumerable<Pass> passes)
        {
            foreach (var pass in passes)
            {
                if (IsTaxExempt(pass.Vehicle))
                {
                    pass.TaxAmount = 0;
                    pass.IsTaxCalculated = true;
                    continue;
                }

                pass.TaxAmount = _externalTaxService.GetTaxAmount(pass.PassTime, pass.Vehicle, pass.City);
                pass.IsTaxCalculated = true;
            }

            return passes;
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
    }
}