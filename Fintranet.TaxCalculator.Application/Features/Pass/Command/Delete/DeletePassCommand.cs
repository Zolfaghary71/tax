using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Command.Delete
{
    public class DeletePassCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}