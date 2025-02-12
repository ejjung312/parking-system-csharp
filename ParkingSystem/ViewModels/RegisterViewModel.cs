using Commands;
using ParkingSystem.Commands;
using ParkingSystem.State.Authenticators;
using ParkingSystem.State.Navigators;
using System.Windows.Input;

namespace ParkingSystem.ViewModels
{
    public class RegisterViewModel : ViewModelBase
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
			}
		}

		private string _username;
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanRegister));
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
                OnPropertyChanged(nameof(CanRegister));
            }
		}

		private string _confirmPassword;
		public string ConfirmPassword
		{
			get
			{
				return _confirmPassword;
			}
			set
			{
				_confirmPassword = value;
				OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanRegister));
            }
		}

		public bool CanRegister => !string.IsNullOrEmpty(Userid) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);

		public ICommand RegisterCommand { get; }
		public ICommand ViewLoginCommand { get; }

        public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator, IRenavigator loginRenavigator)
        {
			RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
			ViewLoginCommand = new RenavigateCommand(loginRenavigator);
        }
    }
}
