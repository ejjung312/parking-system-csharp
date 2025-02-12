using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSystem.Services;
using ParkingSystem.State.Authenticators;
using ParkingSystem.State.Navigators;
using ParkingSystem.ViewModels;
using ParkingSystem.ViewModels.Factories;

namespace ParkingSystem.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient(CreateParkingViewModel);
                services.AddTransient<LoggingViewModel>();
                services.AddTransient<MainViewModel>();

                services.AddSingleton<CreateViewModel<ParkingViewModel>>(services => () => services.GetRequiredService<ParkingViewModel>());
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();

                services.AddSingleton<CreateViewModel<LoggingViewModel>>(services => () => services.GetRequiredService<LoggingViewModel>());

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton<ViewModelDelegateRenavigator<ParkingViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<LoggingViewModel>>();
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
            });

            return host;
        }

        private static ParkingViewModel CreateParkingViewModel(IServiceProvider services)
        {
            return new ParkingViewModel(services.GetRequiredService<ILicensePlateService>(), services.GetRequiredService<IParkingMonitoringService>());
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(services.GetRequiredService<IAuthenticator>(), 
                services.GetRequiredService<ViewModelDelegateRenavigator<ParkingViewModel>>(), services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(services.GetRequiredService<IAuthenticator>(), 
                services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(), services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
        }
    }
}
