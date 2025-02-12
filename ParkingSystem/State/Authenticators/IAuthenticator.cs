using ParkingSystem.Domain.Models;
using ParkingSystem.Domain.Services.AuthenticationServices;

namespace ParkingSystem.State.Authenticators
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        event Action StateChanged;
        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(string userid, string username, string password, string confirmPassword);
        Task Login(string userid, string password);
        void Logout();
    }
}
