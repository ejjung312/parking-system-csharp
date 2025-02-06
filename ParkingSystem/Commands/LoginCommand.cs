using ParkingSystem.State.Navigators;
using ParkingSystem.ViewModels;

namespace ParkingSystem.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _renavigator = renavigator;

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
            _renavigator.Renavigate();
        }
    }
}
