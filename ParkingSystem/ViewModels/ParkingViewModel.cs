using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using ParkingSystem.Services;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ParkingSystem.ViewModels
{
    public class ParkingViewModel : ViewModelBase
    {
        private readonly IVideoProcessingService _licensePlateService;
        private ImageSource _enterImage;
        private CancellationTokenSource _cts;

        private string _video = "Video/enter.mp4";

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

        public ObservableCollection<string> ImageList { get; set; }

        public ParkingViewModel(IVideoProcessingService licensePlateService)
        {
            _licensePlateService = licensePlateService;
            _licensePlateService.FrameProcessed += frame => EnterImage = frame;

            _cts = new CancellationTokenSource();
            Task.Run(() => _licensePlateService.StartProcessingAsync(_video, _cts.Token));

            ImageList = new ObservableCollection<string>
            {
                "/Video/enter.jpg",
                "/Video/enter.jpg",
                "/Video/enter.jpg",
            };
        }

        public override void Dispose()
        {
            _cts.Cancel();

            base.Dispose();
        }
    }
}
