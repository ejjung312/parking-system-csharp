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

        private int _currentPage = 0;
        private const int PageSize = 8;

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

        public async Task<IEnumerable<Vehicle>> GetLoadMoreVehicle(string step)
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                if (step.Equals("p"))
                {
                    if (_currentPage <= 0) _currentPage = 0;
                    else  _currentPage -= 1;
                }

                IEnumerable<Vehicle> entities = await context.Set<Vehicle>()
                                                    .OrderBy(e => e.Id) // 정렬
                                                    .Skip(_currentPage * PageSize).Take(PageSize) // 현재 페이지에 해당하는 50개 항목만
                                                    .ToListAsync();
                if (step.Equals("n"))
                {
                    if (entities.Count() < PageSize)
                    {
                        
                    }
                    else _currentPage++;
                }

                return entities;
            }
        }

        public async Task<Vehicle> Update(int id, Vehicle entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
