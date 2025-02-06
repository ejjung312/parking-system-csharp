using ParkingSystem.ViewModels;

namespace ParkingSystem.State.Navigators
{
    public enum ViewType
    {
        Login,
        Parking,
        Logging
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
