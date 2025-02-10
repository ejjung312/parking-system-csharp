using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using ParkingSystem.API.Services;
using ParkingSystem.Services;
using System.IO;
using System.Windows.Media.Imaging;

namespace Services
{
    public class ParkingMonitoringService : IParkingMonitoringService
    {
        private IParkingDetectionService _parkingDetectionService;
        public event Action<BitmapSource> FrameProcessed;

        public ParkingMonitoringService(IParkingDetectionService parkingDetectionService)
        {
            _parkingDetectionService = parkingDetectionService;
        }

        public async Task StartProcessingAsync(string videoPath, CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
            {
                string video = Path.Combine(Directory.GetCurrentDirectory(), videoPath);

                using var capture = new VideoCapture(video);
                if (!capture.IsOpened()) return;

                Mat frame = new Mat();
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (!capture.Read(frame) || frame.Empty()) break;

                    byte[] responseImage = await _parkingDetectionService.SendFrame(frame);
                    Mat resultMat = Mat.FromImageData(responseImage, ImreadModes.Color);

                    BitmapSource bitmapSource = resultMat.ToBitmapSource();
                    bitmapSource.Freeze();

                    FrameProcessed?.Invoke(bitmapSource);

                    Thread.Sleep(33);
                }
            }, cancellationToken);
        }
    }
}
