﻿using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services.LicensePlateServices
{
    public enum EnterResult
    {
        Success,
        AlreadyEnter,
    }

    public interface IVehicleService
    {
        public Task<EnterResult> EnterVehicle(byte[] licensePlateImg, string licenseNumber);

        public Task<IEnumerable<Vehicle>> GetVehicleHistory(string step);
    }
}
