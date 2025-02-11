using ParkingSystem.Domain.Models;
using ParkingSystem.Domain.Services.AuthenticationServices;
using State.Accounts;

namespace State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public User CurrentUser
        {
            get
            {
                return _accountStore.CurrentUser;
            }
            private set
            {
                _accountStore.CurrentUser = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;

        public event Action StateChanged;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        public async Task Login(string userid, string password)
        {
            CurrentUser = await _authenticationService.Login(userid, password);
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<RegistrationResult> Register(string userid, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(userid, username, password, confirmPassword);
        }
    }
}
