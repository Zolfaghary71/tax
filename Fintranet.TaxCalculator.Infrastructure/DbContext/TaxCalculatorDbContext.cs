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
        
        
       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<TaxRule>().HasData(
        new TaxRule { StartTime = new TimeSpan(6, 0, 0), EndTime = new TimeSpan(6, 29, 0), Amount = 8 },
        new TaxRule { StartTime = new TimeSpan(6, 30, 0), EndTime = new TimeSpan(6, 59, 0), Amount = 13 },
        new TaxRule { StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(7, 59, 0), Amount = 18 },
        new TaxRule { StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(8, 29, 0), Amount = 13 },
        new TaxRule { StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(14, 59, 0), Amount = 8 },
        new TaxRule { StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(15, 29, 0), Amount = 13 },
        new TaxRule { StartTime = new TimeSpan(15, 30, 0), EndTime = new TimeSpan(16, 59, 0), Amount = 18 },
        new TaxRule { StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(17, 59, 0), Amount = 13 },
        new TaxRule { StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(18, 29, 0), Amount = 8 },
        new TaxRule { StartTime = new TimeSpan(18, 30, 0), EndTime = new TimeSpan(5, 59, 0), Amount = 0 }
    );

    var vehicle = new Vehicle()
    {
        Id = Guid.NewGuid(), VehicleType = VehicleType.Car, RegistrationNumber = "ABC123", IsExempt = false,
        CreatedDate = DateTime.UtcNow
    };

    modelBuilder.Entity<Vehicle>().HasData(vehicle);

    modelBuilder.Entity<Pass>().HasData(
        new Pass { PassTime = new DateTime(2013, 1, 14, 21, 0, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 1, 15, 21, 0, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 7, 6, 23, 27), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 7, 15, 27, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 6, 27, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 6, 20, 27), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 14, 35, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 15, 29, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 15, 47, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 16, 1, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 16, 48, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 17, 49, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 18, 29, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 2, 8, 18, 35, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 3, 26, 14, 25, 0), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle },
        new Pass { PassTime = new DateTime(2013, 3, 28, 14, 7, 27), City = City.Gothenburg, CreatedDate = DateTime.UtcNow, Vehicle = vehicle }
    );
}
    }
    
}