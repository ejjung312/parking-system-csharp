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

            if (user.PasswordHash != password)
            {
                throw new InvalidPasswordException(userid, password);
            }

            // TODO - 회원가입 후 다시 테스트
            //PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

            //if (passwordResult != PasswordVerificationResult.Success)
            //{
            //    throw new InvalidPasswordException(userid, password);
            //}

            return user;
        }

        public Task<RegistrationResult> Register(string userid, string username, string password, string confirmPassowrd)
        {
            throw new NotImplementedException();
        }
    }
}
