using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSystem.API.Services;
using ParkingSystem.Domain.Services;
using ParkingSystem.Domain.Services.AuthenticationServices;
using ParkingSystem.EntityFramework.Services;
using ParkingSystem.Services;
using Services;
using State.Authenticators;

namespace ParkingSystem.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IUserService, UserDataService>();
                services.AddSingleton<IPasswordHasher, PasswordHasher>();

                services.AddSingleton<ILicensePlateService, LicensePlateService>();
                services.AddSingleton<IParkingMonitoringService, ParkingMonitoringService>();
                services.AddSingleton<ILicensePlateDetectionService, LicensePlateDetectionService>();
                services.AddSingleton<IParkingDetectionService, ParkingDetectionService>();
            });

            return host;
        }
    }
}
