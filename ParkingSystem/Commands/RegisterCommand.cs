using ParkingSystem.Domain.Services.AuthenticationServices;
using ParkingSystem.State.Authenticators;
using ParkingSystem.State.Navigators;
using ParkingSystem.ViewModels;
using System.Windows;

namespace ParkingSystem.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator registerRenavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = registerRenavigator;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _registerViewModel.CanRegister && base.CanExecute(parameter);
        }

        private void RegisterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
            {
                OnCanExecuteChange();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                RegistrationResult registrationResult = await _authenticator.Register(_registerViewModel.Userid, _registerViewModel.Username, _registerViewModel.Password, _registerViewModel.ConfirmPassword);

                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        _registerRenavigator.Renavigate();
                        break;
                    case RegistrationResult.PasswordDoNotMatch:
                        MessageBox.Show("Password does not match confirm password.");
                        break;
                    case RegistrationResult.IdAlreadyExists:
                        MessageBox.Show("An account for this id already exists.");
                        break;
                    default:
                        MessageBox.Show("Registration failed.");
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Registration failed.");
                throw;
            }
        }
    }
}
