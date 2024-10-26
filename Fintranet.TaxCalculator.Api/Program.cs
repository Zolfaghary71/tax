using Fintranet.TaxCalculator.Application;
using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.DomainServices.Implementations;
using Fintranet.TaxCalculator.Infrastructure.Repositories;
using Fintranet.TaxCalculator.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Fintranet.TaxCalculator.Application.Features.Passes.Queries.GetAll;

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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaxCalculatorDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();