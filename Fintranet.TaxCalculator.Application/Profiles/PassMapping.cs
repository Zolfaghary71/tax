using Fintranet.TaxCalculator.Application.Features.Passes.Command.Create;
using Fintranet.TaxCalculator.Application.Features.Passes.Command.Delete;
using Fintranet.TaxCalculator.Application.Features.Passes.Command.Update;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Application.Profiles
{
    public static class PassMappingExtensions
    {
        public static CreatePassViewModel ToCreatePassViewModel(this Pass pass)
        {
            return new CreatePassViewModel
            {
                PassTime = pass.PassDateTime,
                VehicleId = pass.VehicleId,
                City = pass.City
            };
        }

        public static UpdatePassViewModel ToUpdatePassViewModel(this Pass pass)
        {
            return new UpdatePassViewModel
            {
                Id = pass.Id,
                PassTime = pass.PassDateTime,
                VehicleId = pass.VehicleId,
                City = pass.City
            };
        }

        public static DeletePassViewModel ToDeletePassViewModel(this Pass pass)
        {
            return new DeletePassViewModel
            {
                Id = pass.Id
            };
        }

        public static Pass ToPass(this CreatePassViewModel viewModel)
        {
            return new Pass
            {
                PassDateTime = viewModel.PassTime,
                VehicleId = viewModel.VehicleId,
                City = viewModel.City
            };
        }

        public static Pass ToPass(this UpdatePassViewModel viewModel)
        {
            return new Pass
            {
                Id = viewModel.Id,
                PassDateTime = viewModel.PassTime,
                VehicleId = viewModel.VehicleId,
                City = viewModel.City
            };
        }

        public static Pass ToPass(this DeletePassViewModel viewModel)
        {
            return new Pass
            {
                Id = viewModel.Id
            };
        }
        public static Pass ToPass(this CreatePassCommand command)
        {
            return new Pass
            {
                PassDateTime = command.PassTime,
                VehicleId = command.VehicleId,
                City = command.City
            };
        }
    }
}