using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface IPassRepository
    {
        Task<IEnumerable<Pass>> GetAllAsync();
        Task<Pass> GetByIdAsync(int id);
        Task AddAsync(Pass pass);
        Task UpdateAsync(Pass pass);
        Task DeleteAsync(int id);
    }
}