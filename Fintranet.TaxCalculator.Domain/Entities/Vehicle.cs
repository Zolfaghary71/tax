using Fintranet.TaxCalculator.Domain.Common;

namespace Fintranet.TaxCalculator.Domain.Entities;

public class Vehicle:BaseEntity
{
    public VehicleType VehicleType { get; set; }
    public string RegistrationNumber { get; set; }
    public bool IsExempt { get; set; }
}