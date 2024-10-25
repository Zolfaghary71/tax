using Microsoft.EntityFrameworkCore;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Infrastructure
{
    public class TaxCalculatorDbContext : DbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options) { }

        public DbSet<Pass> Passes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TaxRule> TaxRules { get; set; }
    }
}