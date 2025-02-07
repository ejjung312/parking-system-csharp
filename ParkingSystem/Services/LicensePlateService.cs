using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using SkiaSharp;
using System.IO;
using System.Windows.Media.Imaging;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Models;
using YoloDotNet.Extensions;

namespace ParkingSystem.Services
{
    public class LicensePlateService : IVideoProcessingService
    {
        private string _yoloModel = Path.Combine(Directory.GetCurrentDirectory(), "Onnx/yolo11n.onnx");
        private string _licensePlateModel = Path.Combine(Directory.GetCurrentDirectory(), "Onnx/license_plate_best.onnx");

        public event Action<BitmapSource> FrameProcessed;

        private Yolo _yolo;
        private Yolo _license_detector;

        public LicensePlateService()
        {
            _yolo = new Yolo(new YoloOptions
            {
                OnnxModel = _yoloModel,
                ModelType = ModelType.ObjectDetection,
                Cuda = false,
            });

            _license_detector = new Yolo(new YoloOptions
            {
                OnnxModel = _licensePlateModel,
                ModelType = ModelType.ObjectDetection,
                Cuda = false,
            });
        }

        public async Task StartProcessingAsync(string videoPath, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                string video = Path.Combine(Directory.GetCurrentDirectory(), videoPath);

                using var capture = new VideoCapture(video);
                if (!capture.IsOpened()) return;

                Mat frame = new Mat();
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (!capture.Read(frame) || frame.Empty()) break;

                    frame = prediction(frame);

                    BitmapSource bitmapSource = frame.ToBitmapSource();
                    bitmapSource.Freeze(); // UI Thread에서 사용하기 위해 Freeze()

                    FrameProcessed?.Invoke(bitmapSource); // 프레임 전달

                    Thread.Sleep(33); // 약 30fps 기준 (조정 가능)
                }
            }, cancellationToken);
        }

        private Mat prediction(Mat frame)
        {
            // OpenCV Mat을 BGRA로 변환 (SkiaSharp은 기본적으로 BGRA 사용)
            Mat bgraMat = new Mat();
            Cv2.CvtColor(frame, bgraMat, ColorConversionCodes.BGR2BGRA);

            // Mat의 데이터 포인터 가져오기 (nint 사용)
            nint pixelPtr = bgraMat.Data;

            // SKBitmap 생성
            var skBitmap = new SKBitmap(frame.Width, frame.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
            skBitmap.InstallPixels(skBitmap.Info, pixelPtr, skBitmap.RowBytes);

            // SKImage로 변환
            using var image = SKImage.FromBitmap(skBitmap);

            // Run inference and get the results
            var results = _yolo.RunObjectDetection(image, confidence: 0.25, iou: 0.7);

            // Draw results
            using var resultImage = image.Draw(results);

            using var bitmap = SKBitmap.FromImage(resultImage);

            // SKBitmap 데이터를 바이트 배열로 변환
            IntPtr pixels = bitmap.GetPixels();
            int width = bitmap.Width;
            int height = bitmap.Height;
            int channels = bitmap.ColorType == SKColorType.Gray8 ? 1 : 4; // RGBA 또는 Grayscale

            // Mat 객체 생성
            Mat mat = Mat.FromPixelData(height, width, channels == 4 ? MatType.CV_8UC4 : MatType.CV_8UC1, pixels);

            // OpenCV에서는 기본적으로 BGR을 사용하므로 RGBA → BGR 변환
            if (channels == 4)
            {
                Cv2.CvtColor(mat, mat, ColorConversionCodes.RGBA2BGR);
            }

            return mat;
        }
    }
}
