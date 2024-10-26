using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Queries.GetById
{
    public class GetPassByIdQuery : IRequest<Domain.Entities.Pass>
    {
        public Guid Id { get; set; }
    }
}