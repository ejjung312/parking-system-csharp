using Commands;
using ParkingSystem.Domain.Models;
using ParkingSystem.Domain.Services.LicensePlateServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ParkingSystem.ViewModels
{
    public class LoggingViewModel : ViewModelBase
    {
        private ObservableCollection<Vehicle> _logList;
        public ObservableCollection<Vehicle> LogList
        {
            get => _logList;
            set
            {
                _logList = value;
                OnPropertyChanged(nameof(LogList));
            }
        }

        public ICommand LoggingCommand { get; }

        public LoggingViewModel(IVehicleService vehicleService)
        {
            LogList = new ObservableCollection<Vehicle>();

            // 스크롤 이벤트를 처리하는 ICommand
            LoggingCommand = new LoggingCommand(this, vehicleService);
            LoggingCommand.Execute("n");
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
