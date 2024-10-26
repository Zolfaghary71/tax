using Fintranet.TaxCalculator.Domain.Common;

namespace Fintranet.TaxCalculator.Domain.Entities;

public class Pass:BaseEntity
{
    public DateTime PassDateTime { get; set; }
    public Vehicle Vehicle { get; set; }
    public City City { get; set; }
    public decimal? SupposedTax { get; set; }
    public bool IsTaxCalculated { get; set; }
    public bool IsTheHighestTax { get; set; }
    public decimal ActualTax { get; set; }
}