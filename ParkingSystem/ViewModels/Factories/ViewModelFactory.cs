using ParkingSystem.State.Navigators;

namespace ParkingSystem.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<ParkingViewModel> _createParkingViewModel;
        private readonly CreateViewModel<LoggingViewModel> _createLoggingViewModel;

        public ViewModelFactory(CreateViewModel<ParkingViewModel> createParkingViewModel, CreateViewModel<LoggingViewModel> createLoggingViewModel, CreateViewModel<LoginViewModel> createLoginViewModel)
        {
            _createParkingViewModel = createParkingViewModel;
            _createLoggingViewModel = createLoggingViewModel;
            _createLoginViewModel = createLoginViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Parking:
                    return _createParkingViewModel();
                case ViewType.Logging:
                    return _createLoggingViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
