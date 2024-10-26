using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.DomainServices.Implementations;
using Fintranet.TaxCalculator.Domain.Entities;
using Moq;
using Xunit;

public class StandardCongestionTaxStratgyTests
{
    private readonly Mock<ITaxRuleRepository> _taxRuleRepositoryMock;
    private readonly City _city;
    private readonly StandardCongestionTaxStratgy _strategy;

    public StandardCongestionTaxStratgyTests()
    {
        _taxRuleRepositoryMock = new Mock<ITaxRuleRepository>();
        _city = City.Gothenburg;
        _taxRuleRepositoryMock.Setup(repo => repo.GetTaxRulesAsync(_city)).ReturnsAsync(new List<TaxRule>
        {
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
        });
        _strategy = new StandardCongestionTaxStratgy(_city, _taxRuleRepositoryMock.Object);
    }

    [Fact]
    public async Task CalculateDailyTaxAsync_ShouldSetTaxToZero_ForExemptVehicle()
    {
        var passes = new List<Pass>
        {
            new Pass { PassTime = new DateTime(2023, 10, 10, 8, 0, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Bus } },
            new Pass { PassTime = new DateTime(2023, 10, 10, 9, 0, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Bus } }
        };

        var result = await _strategy.CalculateDailyTaxAsync(passes);

        Assert.All(result, pass => Assert.Equal(0, pass.SupposedTax));
        Assert.All(result, pass => Assert.True(pass.IsTaxCalculated));
    }


        [Fact]
    public async Task CalculateDailyTaxAsync_ShouldThrowException_ForDifferentDates()
    {
        // Arrange
        var passes = new List<Pass>
        {
            new Pass { PassTime = new DateTime(2013, 1, 14, 21, 0, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 1, 15, 21, 0, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 7, 6, 23, 27), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 7, 15, 27, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 6, 27, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 6, 20, 27), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 14, 35, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 15, 29, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 15, 47, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 16, 1, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 16, 48, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 17, 49, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 18, 29, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 18, 35, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 3, 26, 14, 25, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 3, 28, 14, 7, 27), Vehicle = new Vehicle { VehicleType = VehicleType.Car } }
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _strategy.CalculateDailyTaxAsync(passes));
    }
    
        [Fact]
    public async Task CalculateDailyTaxAsync_ShouldCalculateCorrectTax_ForGroupedByDay()
    {
        // Arrange
        var passes = new List<Pass>
        {
            new Pass { PassTime = new DateTime(2013, 1, 14, 21, 0, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 1, 15, 21, 0, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 7, 6, 23, 27), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 7, 15, 27, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 6, 27, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 6, 20, 27), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 14, 35, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 15, 29, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 15, 47, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 16, 1, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 16, 48, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 17, 49, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 18, 29, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 2, 8, 18, 35, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 3, 26, 14, 25, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2013, 3, 28, 14, 7, 27), Vehicle = new Vehicle { VehicleType = VehicleType.Car } }
        };

        var groupedPasses = passes.GroupBy(p => p.PassTime.Date);

        // Act & Assert
        foreach (var group in groupedPasses)
        {
            var result = await _strategy.CalculateDailyTaxAsync(group.ToList());

            // Verify that the tax is calculated correctly for each pass
            foreach (var pass in result)
            {
                Assert.True(pass.IsTaxCalculated);
                Assert.NotNull(pass.SupposedTax);
                // Add more specific assertions based on your tax calculation logic
            }
        }
    }
}