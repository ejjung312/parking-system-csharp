using ParkingSystem.Domain.Models;

namespace State.Accounts
{
    public class AccountStore : IAccountStore
    {
        private User _currentUser;
        public User CurrentUser 
        { 
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
