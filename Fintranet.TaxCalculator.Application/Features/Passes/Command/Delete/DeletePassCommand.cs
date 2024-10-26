using Fintranet.TaxCalculator.Domain.Entities;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Delete
{
    public class DeletePassCommand : IRequest<Pass>
    {
        public Guid Id { get; set; }
    }
}