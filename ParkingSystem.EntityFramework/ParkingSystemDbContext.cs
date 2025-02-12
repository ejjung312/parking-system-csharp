using Microsoft.EntityFrameworkCore;
using ParkingSystem.Domain.Models;

namespace ParkingSystem.EntityFramework
{
    public class ParkingSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public ParkingSystemDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
