namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.CalculateTaxes;

public class VehicleTaxViewModel
{
    public List<PassCalculationViewModel> Passes { get; set; } = new();
    public decimal TotalTax { get; set; }
}