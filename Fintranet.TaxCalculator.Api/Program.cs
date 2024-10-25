using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.DomainServices.Implementations;
using Fintranet.TaxCalculator.Infrastructure.Repositories;
using Fintranet.TaxCalculator.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaxCalculatorDbContext>(options =>
    options.UseInMemoryDatabase("TaxCalculatorDb"));

builder.Services.AddScoped<IPassRepository, PassRepository>();
builder.Services.AddScoped<ITaxRuleRepository, TaxRuleRepository>();

builder.Services.AddTransient<StandardCongestionTaxStratgy>();
builder.Services.AddTransient<ExternalTaxCalculationStrategy>();

builder.Services.AddSingleton<ICongestionTaxStrategyFactory, CongestionTaxStrategyFactory>();

builder.Services.AddSingleton<TaxCalculationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();