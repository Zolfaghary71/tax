using Fintranet.TaxCalculator.Domain.Entities;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Create
{
    public class CreatePassCommand : IRequest<Domain.Entities.Pass>
    {
        public DateTime PassTime { get; set; }
        public Guid VehicleId { get; set; }
        public City City { get; set; }
    }
}