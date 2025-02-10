using ParkingSystem.Services;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ParkingSystem.ViewModels
{
    public class ParkingViewModel : ViewModelBase
    {
        private readonly ILicensePlateService _licensePlateService;
        private readonly IParkingMonitoringService _parkingMonitoringService;
        private ImageSource _enterImage;
        private ImageSource _parkingImage;
        private CancellationTokenSource _ctnEnter;
        private CancellationTokenSource _ctsParking;

        private string _enterVideo = "Video/enter.mp4";
        private string _parkingVideo = "Video/parking.mp4";

        public ImageSource EnterImage
        {
            get
            {
                return _enterImage;
            }
            set
            {
                _enterImage = value;
                OnPropertyChanged(nameof(EnterImage));
            }
        }

        public ImageSource ParkingImage
        {
            get
            {
                return _parkingImage;
            }
            set
            {
                _parkingImage = value;
                OnPropertyChanged(nameof(ParkingImage));
            }
        }

        public ObservableCollection<string> ImageList { get; set; }

        public ParkingViewModel(ILicensePlateService licensePlateService, IParkingMonitoringService parkingMonitoringService)
        {
            _licensePlateService = licensePlateService;
            _parkingMonitoringService = parkingMonitoringService;

            _licensePlateService.FrameProcessed += frame => EnterImage = frame;
            _parkingMonitoringService.FrameProcessed += frame => ParkingImage = frame;

            _ctnEnter = new CancellationTokenSource();
            Task.Run(() => _licensePlateService.StartProcessingAsync(_enterVideo, _ctnEnter.Token));

            _ctsParking = new CancellationTokenSource();
            Task.Run(() => _parkingMonitoringService.StartProcessingAsync(_parkingVideo, _ctsParking.Token));

            ImageList = new ObservableCollection<string>
            {
                "/Video/enter.jpg",
                "/Video/enter.jpg",
                "/Video/enter.jpg",
            };
        }

        public override void Dispose()
        {
            _ctnEnter.Cancel();
            _ctsParking.Cancel();

            base.Dispose();
        }
    }
}
