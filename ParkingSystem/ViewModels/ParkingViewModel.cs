using System.Collections.ObjectModel;

namespace ParkingSystem.ViewModels
{
    public class ParkingViewModel : ViewModelBase
    {
        public ObservableCollection<string> ImageList { get; set; }

        public ParkingViewModel()
        {
            ImageList = new ObservableCollection<string>
            {
                "/Video/enter.jpg",
                "/Video/enter.jpg",
                "/Video/enter.jpg",
            };
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
