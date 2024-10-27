using Fintranet.TaxCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculator.Infrastructure.DbContext
{
    public class TaxCalculatorDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options)
        {
        }

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

            var vehicleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var vehicle = new Vehicle
            {
                Id = vehicleId,
                RegistrationNumber = "123",
                VehicleType = VehicleType.Car,
                CreatedDate = DateTime.UtcNow
            };

            modelBuilder.Entity<Vehicle>().HasData(vehicle);

            modelBuilder.Entity<Pass>().HasData(
                new Pass
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    PassDateTime = new DateTime(2013, 1, 14, 21, 0, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    PassDateTime = new DateTime(2013, 1, 15, 21, 0, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    PassDateTime = new DateTime(2013, 2, 7, 6, 23, 27),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    PassDateTime = new DateTime(2013, 2, 7, 15, 27, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    PassDateTime = new DateTime(2013, 2, 8, 6, 27, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    PassDateTime = new DateTime(2013, 2, 8, 6, 20, 27),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    PassDateTime = new DateTime(2013, 2, 8, 14, 35, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    PassDateTime = new DateTime(2013, 2, 8, 15, 29, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    PassDateTime = new DateTime(2013, 2, 8, 15, 47, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    PassDateTime = new DateTime(2013, 2, 8, 16, 1, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    PassDateTime = new DateTime(2013, 2, 8, 16, 48, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    PassDateTime = new DateTime(2013, 2, 8, 17, 49, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    PassDateTime = new DateTime(2013, 2, 8, 18, 29, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    PassDateTime = new DateTime(2013, 2, 8, 18, 35, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    PassDateTime = new DateTime(2013, 3, 26, 14, 25, 0),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                },
                new Pass
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    PassDateTime = new DateTime(2013, 3, 28, 14, 7, 27),
                    City = City.Gothenburg,
                    CreatedDate = DateTime.UtcNow,
                    VehicleId = vehicleId
                }
            );
        }
    }
}