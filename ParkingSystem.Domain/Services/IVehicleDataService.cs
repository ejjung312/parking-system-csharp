using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services
{
    public interface IVehicleDataService : IDataService<Vehicle>
    {
        Task<Vehicle> GetLicenseNumber(string licenseNumber);
    }
}
