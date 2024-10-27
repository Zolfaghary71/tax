using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface IPassRepository
    {
        Task<Pass> GetByIdAsync(Guid id);
        Task<IEnumerable<Pass>> GetAllAsync();
        Task AddAsync(Pass pass);
        Task UpdateAsync(Pass pass);
        Task DeleteAsync(Pass pass);
        Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);
        Task<IEnumerable<Pass>> GetPassesByVehicleIdAndCityAsync(Guid vehicleId,City city); 
        
    }
}