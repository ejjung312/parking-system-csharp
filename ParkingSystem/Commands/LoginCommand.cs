using ParkingSystem.Domain.Exceptions;
using ParkingSystem.State.Authenticators;
using ParkingSystem.State.Navigators;
using ParkingSystem.ViewModels;
using System.Windows;

namespace ParkingSystem.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _renavigator = renavigator;
            _authenticator = authenticator;

            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _loginViewModel.CanLogin && base.CanExecute(parameter);
        }

        private void LoginViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.CanLogin))
            {
                OnCanExecuteChange();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _authenticator.Login(_loginViewModel.Userid, _loginViewModel.Password);

                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                MessageBox.Show("Username does not exist.");
            }
            catch (InvalidPasswordException)
            {
                MessageBox.Show("Incorrect password.");
            }
            catch (Exception)
            {
                MessageBox.Show("Login failed.");
            }
        }
    }
}
