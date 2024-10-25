using Fintranet.TaxCalculator.Domain.Common;

namespace Fintranet.TaxCalculator.Domain.Entities;

public class Pass:BaseEntity
{
    public DateTime PassTime { get; set; }
    public Vehicle Vehicle { get; set; }
    public City City { get; set; }
    public decimal? TaxAmount { get; set; }
    public TaxDetailType TaxDetail { get; set; }
    public bool IsTaxCalculated { get; set; } 
}