using Microsoft.EntityFrameworkCore;
using ParkingSystem.Domain.Models;
using ParkingSystem.Domain.Services;
using ParkingSystem.EntityFramework.Services.Common;

namespace ParkingSystem.EntityFramework.Services
{
    public class VehicleDataService : IVehicleDataService
    {
        private readonly ParkingSystemDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Vehicle> _nonQueryDataService;


        public VehicleDataService(ParkingSystemDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Vehicle>(contextFactory);
        }

        public async Task<Vehicle> Create(Vehicle entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Vehicle> Get(int id)
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                Vehicle entity = await context.Set<Vehicle>().FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Vehicle> entities = await context.Set<Vehicle>().ToListAsync();

                return entities;
            }
        }

        public async Task<Vehicle> GetLicenseNumber(string licenseNumber)
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                Vehicle entity = await context.Set<Vehicle>().FirstOrDefaultAsync((e) => e.LicenseNumber == licenseNumber);

                return entity;
            }
        }

        public async Task<Vehicle> Update(int id, Vehicle entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
