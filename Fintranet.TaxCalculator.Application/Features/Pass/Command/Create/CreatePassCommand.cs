using Fintranet.TaxCalculator.Domain.Entities;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Create
{
    public class CreatePassCommand : IRequest<Domain.Entities.Pass>
    {
        public DateTime PassTime { get; set; }
        public int VehicleId { get; set; }
        public City City { get; set; }
    }
}