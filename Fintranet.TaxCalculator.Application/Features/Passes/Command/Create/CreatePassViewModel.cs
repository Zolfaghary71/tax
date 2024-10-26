using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Create
{
    public class CreatePassViewModel
    {
        public DateTime PassTime { get; set; }
        public Guid VehicleId { get; set; }
        public City City { get; set; }
    }
}