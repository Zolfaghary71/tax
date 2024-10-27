using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Infrastructure.ExternalServices;

public class ExternalService:IExternalTaxService
{
   public decimal GetTaxAmount(DateTime passTime, Vehicle vehicle, City city)
    {
        throw new NotImplementedException();
    }
}