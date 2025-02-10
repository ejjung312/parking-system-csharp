using OpenCvSharp;

namespace ParkingSystem.API.Services
{
    public interface IParkingDetectionService
    {
        Task<byte[]> SendFrame(Mat frmae);
    }
}
