using Fintranet.TaxCalculator.Domain.Common;

namespace Fintranet.TaxCalculator.Domain.Entities;

public class TaxableVehicle:BaseEntity
{
    public VehicleType VehicleType { get; set; }
    public bool IsExempt { get; set; }
    public List<DateOnly> Passes { get; set; }
}