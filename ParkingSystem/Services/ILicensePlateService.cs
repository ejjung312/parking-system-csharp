using System.Windows.Media.Imaging;

namespace ParkingSystem.Services
{
    public interface ILicensePlateService
    {
        event Action<BitmapSource> FrameProcessed;
        Task StartProcessingAsync(string videoPath, CancellationToken cancellationToken);
    }
}
