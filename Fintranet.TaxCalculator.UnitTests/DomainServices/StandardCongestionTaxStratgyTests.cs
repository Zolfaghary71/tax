using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
}