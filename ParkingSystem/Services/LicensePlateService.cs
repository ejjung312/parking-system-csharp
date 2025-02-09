using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using SkiaSharp;
using System.IO;
using System.Windows.Media.Imaging;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Models;
using YoloDotNet.Extensions;
using ParkingSystem.API.Services;

namespace ParkingSystem.Services
{
    public class LicensePlateService : IVideoProcessingService
    {
        private ILicensePlateDetectionService _licensePlateDetectionService;
        public event Action<BitmapSource> FrameProcessed;

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

                    //frame = prediction(frame);
                    // API 통신
                    byte[] responseImage = await _licensePlateDetectionService.SendFrame(frame);
                    Mat resultMat = Mat.FromImageData(responseImage, ImreadModes.Color);

                    BitmapSource bitmapSource = resultMat.ToBitmapSource();
                    bitmapSource.Freeze(); // UI Thread에서 사용하기 위해 Freeze()

                    FrameProcessed?.Invoke(bitmapSource); // 프레임 전달

                    Thread.Sleep(33); // 약 30fps 기준 (조정 가능)
                }
            }, cancellationToken);
        }
    }
}
