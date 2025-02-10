using System.Windows.Media.Imaging;

namespace ParkingSystem.Services
{
    public interface IParkingMonitoringService
    {
        event Action<BitmapSource> FrameProcessed;
        Task StartProcessingAsync(string videoPath, CancellationToken cancellationToken);
    }
}
