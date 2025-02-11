using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ParkingSystem.EntityFramework
{
    public class ParkingSystemDbContextFactory : IDesignTimeDbContextFactory<ParkingSystemDbContext>
    {
        //private readonly string _connectionString;

        //public ParkingSystemDbContextFactory(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public ParkingSystemDbContext CreateDbContext(string[] args = null)
        {
            string _connectionstring = "Server=127.0.0.1;Database=dbcsharp;User=csharp;Password=csharp!@;";

            var options = new DbContextOptionsBuilder<ParkingSystemDbContext>();

            options.UseMySql(_connectionstring, MySqlServerVersion.AutoDetect(_connectionstring),
                b => b.MigrationsAssembly("ParkingSystem.EntityFramework"));

            return new ParkingSystemDbContext(options.Options);
        }
    }
}
