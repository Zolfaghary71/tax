using Fintranet.TaxCalculator.Application.Features.Passes.Command.CalculateTaxes;
using MediatR;
using Fintranet.TaxCalculator.Domain.DomainServices.Contracts;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Application.Features.Passes.Command.Calculate
{
    public class CalculateTaxesCommandHandler : IRequestHandler<CalculateTaxesCommand, VehicleTaxViewModel>
    {
        private readonly ITaxCalculationService _taxCalculationService;
        private readonly IPassRepository _passRepository;

        public CalculateTaxesCommandHandler(ITaxCalculationService taxCalculationService, IPassRepository passRepository)
        {
            _taxCalculationService = taxCalculationService;
            _passRepository = passRepository;
        }

        public async Task<VehicleTaxViewModel> Handle(CalculateTaxesCommand request, CancellationToken cancellationToken)
        {
            var passes = await _passRepository.GetPassesByVehicleIdAndCityAsync(request.VehicleId,request.City);

            var groupedPasses = passes.GroupBy(p => p.PassDateTime.Date);

            decimal totalTax = 0;
            var passViewModels = new List<PassCalculationViewModel>();

            foreach (var dayPasses in groupedPasses)
            {
                var taxedPasses = _taxCalculationService.CalculateTax(dayPasses.ToList());
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