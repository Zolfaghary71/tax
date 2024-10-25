using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculator.Infrastructure.Repositories
{
    public class TaxRuleRepository : ITaxRuleRepository
    {
        private readonly TaxCalculatorDbContext _context;

        public TaxRuleRepository(TaxCalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxRule>> GetTaxRulesAsync(City city)
        {
            return await _context.TaxRules.Where(r => r.City == city).ToListAsync();
        }
    }
}