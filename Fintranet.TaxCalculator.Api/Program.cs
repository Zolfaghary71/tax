using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.DomainServices.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GothenburgCongestionTaxStratgy>();
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