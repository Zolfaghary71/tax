using Fintranet.TaxCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculator.Infrastructure.DbContext
{
    public class TaxCalculatorDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options) { }

        public DbSet<Pass> Passes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TaxRule> TaxRules { get; set; }
    }
}