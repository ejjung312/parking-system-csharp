using Microsoft.EntityFrameworkCore;
using ParkingSystem.Domain.Models;
using ParkingSystem.Domain.Services;
using ParkingSystem.EntityFramework.Services.Common;

namespace ParkingSystem.EntityFramework.Services
{
    public class UserDataService : IUserService
    {
        private readonly ParkingSystemDbContextFactory _contextFactory;
        private readonly NonQueryDataService<User> _nonQueryDataService;

        public UserDataService(ParkingSystemDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<User>(contextFactory);
        }

        public async Task<User> Create(User entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<User> Get(int id)
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            };
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<User> entities = await context.Set<User>().ToListAsync();

                return entities;
            };
        }

        public async Task<User> GetUserId(string userid)
        {
            using (ParkingSystemDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.UserId == userid);

                return entity;
            };
        }

        public async Task<User> Update(int id, User entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
