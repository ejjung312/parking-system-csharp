using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSystem.API.Services;
using ParkingSystem.Services;

namespace ParkingSystem.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IVideoProcessingService, LicensePlateService>();
                services.AddSingleton<ILicensePlateDetectionService, LicensePlateDetectionService>();
            });

            return host;
        }
    }
}
