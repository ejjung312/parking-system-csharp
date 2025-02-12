using Commands;
using ParkingSystem.Commands;
using ParkingSystem.State.Authenticators;
using ParkingSystem.State.Navigators;
using System.Windows.Input;

namespace ParkingSystem.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
		private string _userid;
		public string Userid
		{
			get
			{
				return _userid;
			}
			set
			{
				_userid = value;
				OnPropertyChanged(nameof(Userid));
                OnPropertyChanged(nameof(CanLogin));
            }
		}

		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanLogin));
            }
		}

		public bool CanLogin => !string.IsNullOrEmpty(Userid) && !string.IsNullOrEmpty(Password);

		public ICommand LoginCommand { get; }
		public ICommand ViewRegisterCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
			ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
