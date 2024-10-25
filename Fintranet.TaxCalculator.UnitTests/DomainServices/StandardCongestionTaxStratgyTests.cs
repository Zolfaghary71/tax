using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.DomainServices.Implementations;
using Fintranet.TaxCalculator.Domain.Entities;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StandardCongestionTaxStratgyTests
{
    private readonly Mock<ITaxRuleRepository> _taxRuleRepositoryMock;
    private readonly StandardCongestionTaxStratgy _strategy;

    public StandardCongestionTaxStratgyTests()
    {
        _taxRuleRepositoryMock = new Mock<ITaxRuleRepository>();
        _taxRuleRepositoryMock.Setup(repo => repo.GetTaxRulesAsync(It.IsAny<City>()))
            .ReturnsAsync(new List<TaxRule>
            {
                new TaxRule { StartTime = new TimeSpan(6, 0, 0), EndTime = new TimeSpan(6, 30, 0), Amount = 8 },
                new TaxRule { StartTime = new TimeSpan(6, 30, 0), EndTime = new TimeSpan(7, 0, 0), Amount = 13 },
                // Add more rules as needed
            });

        var city = City.Gothenburg;
        _strategy = new StandardCongestionTaxStratgy(city, _taxRuleRepositoryMock.Object);
    }

    [Fact]
    public async Task CalculateTaxAsync_ShouldReturnCorrectTaxForSingleDay()
    {
        var passes = new List<Pass>
        {
            new Pass { PassTime = new DateTime(2023, 10, 1, 6, 15, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } },
            new Pass { PassTime = new DateTime(2023, 10, 1, 6, 45, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } }
        };

        var result = await _strategy.CalculateDailyTaxAsync(passes);

        Assert.Equal(2, result.Count());
        Assert.Equal(8, result.First().SupposedTax);
        Assert.Equal(13, result.Last().SupposedTax);
    }



    [Fact]
    public async Task CalculateTaxAsync_ShouldReturnZeroTaxForExemptVehicle()
    {
        var passes = new List<Pass>
        {
            new Pass { PassTime = new DateTime(2023, 10, 1, 6, 15, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Bus } }
        };

        var result = await _strategy.CalculateTaxAsync(passes);

        Assert.Equal(0, result.First().SupposedTax);
    }

    [Fact]
    public async Task CalculateTaxAsync_ShouldReturnZeroTaxForExemptDay()
    {
        var passes = new List<Pass>
        {
            new Pass { PassTime = new DateTime(2023, 7, 1, 6, 15, 0), Vehicle = new Vehicle { VehicleType = VehicleType.Car } }
        };

        var result = await _strategy.CalculateTaxAsync(passes);

        Assert.Equal(0, result.First().SupposedTax);
    }
}