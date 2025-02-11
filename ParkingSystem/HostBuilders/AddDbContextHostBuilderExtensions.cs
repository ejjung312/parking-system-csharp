using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSystem.EntityFramework;

namespace HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, servicees) =>
            {
                string connectionString = context.Configuration.GetConnectionString("default");
                servicees.AddDbContext<ParkingSystemDbContext>(o => o.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly("ParkingSystem.EntityFramework")));

                servicees.AddSingleton<ParkingSystemDbContextFactory>(new ParkingSystemDbContextFactory(connectionString));
            });

            return host;
        }
    }
}
