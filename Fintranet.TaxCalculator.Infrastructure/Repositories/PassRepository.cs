using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using Fintranet.TaxCalculator.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculator.Infrastructure.Repositories
{
    public class PassRepository : IPassRepository
    {
        private readonly TaxCalculatorDbContext _context;

        public PassRepository(TaxCalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pass>> GetAllAsync()
        {
            return await _context.Passes.ToListAsync();
        }

        public async Task<Pass> GetByIdAsync(int id)
        {
            return await _context.Passes.FindAsync(id);
        }

        public async Task AddAsync(Pass pass)
        {
            await _context.Passes.AddAsync(pass);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pass pass)
        {
            _context.Passes.Update(pass);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pass = await _context.Passes.FindAsync(id);
            if (pass != null)
            {
                _context.Passes.Remove(pass);
                await _context.SaveChangesAsync();
            }
        }
    }
}