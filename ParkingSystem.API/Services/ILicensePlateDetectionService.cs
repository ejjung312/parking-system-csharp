using OpenCvSharp;

namespace ParkingSystem.API.Services
{
    public interface ILicensePlateDetectionService
    {
        Task<byte[]> SendFrame(Mat frmae);
    }
}
