using ParkingSystem.State.Navigators;

namespace ParkingSystem.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
