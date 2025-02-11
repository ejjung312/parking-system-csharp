using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services
{
    public interface IUserService : IDataService<User>
    {
        Task<User> GetUserId(string userid);
    }
}
