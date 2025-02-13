using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services.LicensePlateServices
{
    public enum EnterResult
    {
        Success,
        AlreadyEnter,
    }

    public interface IVehicleService
    {
        public Task<EnterResult> EnterVehicle(string licenseNumber);
    }
}
