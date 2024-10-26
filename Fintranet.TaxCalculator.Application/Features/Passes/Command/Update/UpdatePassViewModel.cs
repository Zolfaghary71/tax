using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Update
{
    public class UpdatePassViewModel
    {
        public Guid Id { get; set; }
        public DateTime PassTime { get; set; }
        public Guid VehicleId { get; set; }
        public City City { get; set; }
    }
}