using Fintranet.TaxCalculator.Domain.Entities;
using MediatR;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.CalculateTaxes;

public class CalculateTaxesCommand : IRequest<VehicleTaxViewModel>
{
    public Guid VehicleId { get; set; }
    public City City { get; set; }
}