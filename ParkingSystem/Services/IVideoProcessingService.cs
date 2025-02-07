using System.Windows.Media.Imaging;

namespace ParkingSystem.Services
{
    public interface IVideoProcessingService
    {
        event Action<BitmapSource> FrameProcessed;
        Task StartProcessingAsync(string videoPath, CancellationToken cancellationToken);
    }
}
