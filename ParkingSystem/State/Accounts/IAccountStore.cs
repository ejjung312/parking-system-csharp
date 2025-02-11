using ParkingSystem.Domain.Models;

namespace State.Accounts
{
    public interface IAccountStore
    {
        User CurrentUser { get; set; }
        event Action StateChanged;
    }
}
