﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ParkingSystem.EntityFramework
{
    public class ParkingSystemDbContextFactory : IDesignTimeDbContextFactory<ParkingSystemDbContext>
    {
        private readonly string _connectionString;

        public ParkingSystemDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ParkingSystemDbContext CreateDbContext(string[] args = null)
        {
            //string _connectionString = "Server=127.0.0.1;Database=dbcsharp;User=csharp;Password=csharp!@;";

            var options = new DbContextOptionsBuilder<ParkingSystemDbContext>();

            options.UseMySql(_connectionString, MySqlServerVersion.AutoDetect(_connectionString),
                b => b.MigrationsAssembly("ParkingSystem.EntityFramework"));

            return new ParkingSystemDbContext(options.Options);
        }
    }
}
