using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSystem.API.Services;
using ParkingSystem.Services;
using Services;

namespace ParkingSystem.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<ILicensePlateService, LicensePlateService>();
                services.AddSingleton<IParkingMonitoringService, ParkingMonitoringService>();
                services.AddSingleton<ILicensePlateDetectionService, LicensePlateDetectionService>();
                services.AddSingleton<IParkingDetectionService, ParkingDetectionService>();
            });

            return host;
        }
    }
}
