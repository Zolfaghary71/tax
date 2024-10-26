using Fintranet.TaxCalculator.Application.Features.Passes.Command.CalculateTaxes;
using MediatR;
using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Calculate
{
    public class CalculateTaxesCommandHandler : IRequestHandler<CalculateTaxesCommand, VehicleTaxViewModel>
    {
        private readonly ICongestionTaxStrategyFactory _taxStrategyFactory;
        private readonly IPassRepository _passRepository;

        public CalculateTaxesCommandHandler(ICongestionTaxStrategyFactory taxStrategyFactory, IPassRepository passRepository)
        {
            _taxStrategyFactory = taxStrategyFactory;
            _passRepository = passRepository;
        }

        public async Task<VehicleTaxViewModel> Handle(CalculateTaxesCommand request, CancellationToken cancellationToken)
        {
            var passes = await _passRepository.GetPassesByVehicleIdAsync(request.VehicleId);
            var vehiclePasses = passes.Where(p => p.City == request.City);

            var groupedPasses = vehiclePasses.GroupBy(p => p.PassDateTime.Date);
            var taxStrategy = _taxStrategyFactory.GetStrategy(request.City);

            decimal totalTax = 0;
            var passViewModels = new List<PassCalculationViewModel>();

            foreach (var dayPasses in groupedPasses)
            {
                var taxedPasses = taxStrategy.CalculateDailyTax(dayPasses.ToList());
                totalTax += taxedPasses.Sum(p => p.ActualTax);

                passViewModels.AddRange(taxedPasses.Select(p => new PassCalculationViewModel
                {
                    PassDateTime = p.PassDateTime,
                    ActualTax = p.ActualTax
                }));
            }

            return new VehicleTaxViewModel
            {
                Passes = passViewModels,
                TotalTax = totalTax
            };
        }
    }
}