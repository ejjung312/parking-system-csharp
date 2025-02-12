using Microsoft.AspNet.Identity;
using ParkingSystem.Domain.Exceptions;
using ParkingSystem.Domain.Models;

namespace ParkingSystem.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Login(string userid, string password)
        {
            User user = await _userService.GetUserId(userid);

            if (user == null)
            {
                throw new UserNotFoundException(userid);
            }

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(userid, password);
            }

            return user;
        }

        public async Task<RegistrationResult> Register(string userid, string username, string password, string confirmPassowrd)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassowrd)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }

            User user = await _userService.GetUserId(userid);
            if (user != null)
            {
                result = RegistrationResult.IdAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                User newUser = new User()
                {
                    UserId = userid,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DatedJoined = DateTime.Now
                };

                await _userService.Create(newUser);
            }

            return result;
        }
    }
}
