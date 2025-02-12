using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ParkingSystem.EntityFramework
{
    public class ParkingSystemDbContextFactory : IDesignTimeDbContextFactory<ParkingSystemDbContext>
    {
        private string _connectionString;

        public ParkingSystemDbContextFactory()
        {
            
        }

        public ParkingSystemDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ParkingSystemDbContext CreateDbContext(string[] args = null)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ParkingSystem"))
                .AddJsonFile("appsettings.json")
                .Build();

                _connectionString = configuration.GetConnectionString("default");
            }

            var options = new DbContextOptionsBuilder<ParkingSystemDbContext>();

            options.UseMySql(_connectionString, MySqlServerVersion.AutoDetect(_connectionString),
                b => b.MigrationsAssembly("ParkingSystem.EntityFramework"));

            return new ParkingSystemDbContext(options.Options);
        }
    }
}
