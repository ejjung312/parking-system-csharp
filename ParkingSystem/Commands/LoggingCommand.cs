using ParkingSystem.Commands;
using ParkingSystem.Domain.Models;
using ParkingSystem.Domain.Services.LicensePlateServices;
using ParkingSystem.ViewModels;

namespace Commands
{
    public class LoggingCommand : AsyncCommandBase
    {
        private readonly LoggingViewModel _loggingViewModel;
        private readonly IVehicleService _vehicleService;

        public LoggingCommand(LoggingViewModel loggingViewModel, IVehicleService vehicleService)
        {
            _loggingViewModel = loggingViewModel;
            _vehicleService = vehicleService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is string)
            {
                IEnumerable<Vehicle> list = await _vehicleService.GetVehicleHistory((string)parameter);

                _loggingViewModel.LogList.Clear();

                if (list.Count() <= 0) return;

                foreach (Vehicle vehicle in list)
                {
                    _loggingViewModel.LogList.Add(vehicle);
                }
            }
        }
    }
}
