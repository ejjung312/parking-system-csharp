using ParkingSystem.Commands;
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

        public LoginViewModel(IRenavigator loginRenavigator)
        {
			LoginCommand = new LoginCommand(this, loginRenavigator);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
