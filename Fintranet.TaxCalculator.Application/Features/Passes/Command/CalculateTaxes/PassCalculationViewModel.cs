namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.CalculateTaxes;

public class PassCalculationViewModel
{
    public DateTime PassDateTime { get; set; }
    public decimal ActualTax { get; set; }
    public string FormattedPassDateTime => PassDateTime.ToString("yyyy-MM-dd HH:mm:ss");
}