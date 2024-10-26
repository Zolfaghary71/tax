using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Pass.Queries.GetById
{
    public class GetPassByIdQuery : IRequest<Domain.Entities.Pass>
    {
        public int Id { get; set; }
    }
}