using OpenCvSharp;
using ParkingSystem.API.Results;

namespace ParkingSystem.API.Services
{
    public interface ILicensePlateDetectionService
    {
        Task<ApiResponse> SendFrame(Mat frmae);
    }
}
