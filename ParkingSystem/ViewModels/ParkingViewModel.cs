using ParkingSystem.API.Results;
using ParkingSystem.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ParkingSystem.ViewModels
{
    public class ParkingViewModel : ViewModelBase
    {
        private readonly ILicensePlateService _licensePlateService;
        private readonly IParkingMonitoringService _parkingMonitoringService;
        private ImageSource _enterImage;
        private ImageSource _parkingImage;
        private ObservableCollection<LicensePlateItem> _imageList;
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

        public ObservableCollection<LicensePlateItem> ImageList 
        {
            get => _imageList;
            set
            {
                _imageList = value;
                OnPropertyChanged(nameof(ImageList));
            }
        }

        public ParkingViewModel(ILicensePlateService licensePlateService, IParkingMonitoringService parkingMonitoringService)
        {
            _licensePlateService = licensePlateService;
            _parkingMonitoringService = parkingMonitoringService;

            ImageList = new ObservableCollection<LicensePlateItem>();

            _licensePlateService.FrameProcessed += LicensePlateService_FrameProcessed;
            _parkingMonitoringService.FrameProcessed += frame => ParkingImage = frame;

            _ctnEnter = new CancellationTokenSource();
            Task.Run(() => _licensePlateService.StartProcessingAsync(_enterVideo, _ctnEnter.Token));

            _ctsParking = new CancellationTokenSource();
            Task.Run(() => _parkingMonitoringService.StartProcessingAsync(_parkingVideo, _ctsParking.Token));
        }

        private void LicensePlateService_FrameProcessed(BitmapSource processedImg, BitmapSource licensePlateImg, string licensePlateTxt)
        {
            EnterImage = processedImg;

            if (licensePlateImg != null)
            {
                // UI 스레드에서 호출
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (ImageList.Count >= 7)
                    {
                        ImageList.RemoveAt(0);
                    }
                    ImageList.Add(new LicensePlateItem { LicensePlateImg = licensePlateImg, LicensePlateText = licensePlateTxt });
                });
            }
        }

        public override void Dispose()
        {
            _ctnEnter.Cancel();
            _ctsParking.Cancel();

            base.Dispose();
        }
    }
}
