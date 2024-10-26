using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Delete
{
    public class DeletePassCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}