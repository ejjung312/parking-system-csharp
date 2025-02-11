using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Success,
        PasswordDoNotMatch,
        IdAlreadyExists,
    }

    public interface IAuthenticationService
    {
        public Task<RegistrationResult> Register(string userid, string username, string password, string confirmPassowrd);
        public Task<User> Login(string userid, string password);
    }
}
