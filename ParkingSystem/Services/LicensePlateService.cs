using OpenCvSharp;
using ParkingSystem.API.Results;
using ParkingSystem.API.Services;
using ParkingSystem.Helper;
using System.IO;
using System.Windows.Media.Imaging;

namespace ParkingSystem.Services
{
    public class LicensePlateService : ILicensePlateService
    {
        private ILicensePlateDetectionService _licensePlateDetectionService;
        public event Action<BitmapSource, BitmapSource, String> FrameProcessed;

        public LicensePlateService(ILicensePlateDetectionService licensePlateDetectionService)
        {
            _licensePlateDetectionService = licensePlateDetectionService;
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

                    // API 통신
                    ApiResponse jsonData = await _licensePlateDetectionService.SendFrame(frame);

                    if (jsonData == null) continue;

                    BitmapSource bitmapSource = ImageProcessHelper.getJsonToBitmapSource(jsonData.ProcessedImg);
                    bitmapSource.Freeze(); // UI Thread에서 사용하기 위해 Freeze()

                    BitmapSource licenseBitmapSource = null;
                    if (jsonData.LicensePlateImg != null)
                    {

                        licenseBitmapSource = ImageProcessHelper.getJsonToBitmapSource(jsonData.LicensePlateImg);
                        licenseBitmapSource.Freeze(); // UI Thread에서 사용하기 위해 Freeze()
                    }

                    FrameProcessed?.Invoke(bitmapSource, licenseBitmapSource, jsonData.LicensePlateText); // 프레임 전달

                    Thread.Sleep(33); // 약 30fps 기준 (조정 가능)
                }
            }, cancellationToken);
        }
    }
}
