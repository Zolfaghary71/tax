using Fintranet.TaxCalculator.Application;
using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.DomainServices.Implementations;
using Fintranet.TaxCalculator.Infrastructure.Repositories;
using Fintranet.TaxCalculator.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Fintranet.TaxCalculator.Application.Features.Pass.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaxCalculatorDbContext>(options =>
    options.UseInMemoryDatabase("TaxCalculatorDb"));

builder.Services.AddScoped<IPassRepository, PassRepository>();
builder.Services.AddScoped<ITaxRuleRepository, TaxRuleRepository>();

builder.Services.AddSingleton<ICongestionTaxStrategyFactory, CongestionTaxStrategyFactory>();

builder.Services.AddSingleton<TaxCalculationService>();

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllPassesQueryHandler).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();