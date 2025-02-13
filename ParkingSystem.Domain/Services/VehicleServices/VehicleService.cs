using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services.LicensePlateServices
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleDataService _vehicleDataService;

        public VehicleService(IVehicleDataService vehicleDataService)
        {
            _vehicleDataService = vehicleDataService;
        }

        public async Task<EnterResult> EnterVehicle(byte[] licensePlateImg, string licenseNumber)
        {
            EnterResult result = EnterResult.Success;

            Vehicle vehicle = await _vehicleDataService.GetLicenseNumber(licenseNumber);

            if (vehicle != null)
            {
                result = EnterResult.AlreadyEnter;
            }

            if (result == EnterResult.Success)
            {
                Vehicle newVehicle = new Vehicle()
                {
                    LicenseNumber = licenseNumber,
                    LicensePlateImg = licensePlateImg,
                    EnterDate = DateTime.Now,
                };

                await _vehicleDataService.Create(newVehicle);
            }

            return result;
        }
    }
}
