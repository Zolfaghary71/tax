using Fintranet.TaxCalculator.Domain.Entities;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Update
{
    public class UpdatePassCommand : IRequest<Domain.Entities.Pass>
    {
        public int Id { get; set; }
        public DateTime PassTime { get; set; }
        public int VehicleId { get; set; }
        public City City { get; set; }
    }
}