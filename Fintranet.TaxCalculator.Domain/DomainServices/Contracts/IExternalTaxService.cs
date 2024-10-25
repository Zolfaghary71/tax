using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.Services
{
    public interface IExternalTaxService
    {
        decimal GetTaxAmount(DateTime passTime, Vehicle vehicle, City city);
    }
}