using Microsoft.EntityFrameworkCore;
using TMS2.DAL.Models;

namespace TMS2.DAL.Data
{
    public class Tms2Context : DbContext
    {
        public Tms2Context(DbContextOptions<Tms2Context> options) : base(options)
        {
        }

        public DbSet<Pump> Pumps { get; set; }
        public DbSet<PumpLog> PumpLogs { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorLog> SensorLogs { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<User> Users { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder context)
        {
            context.UseSqlServer("Server=tcp:hooyberghs.database.windows.net,1433;Initial Catalog=hooyberghs-db;Persist Security Info=False;User ID=hooyberghs-admin;Password=H00yberghs!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pump>().ToTable("Pump");
            modelBuilder.Entity<PumpLog>().ToTable("PumpLog");
            modelBuilder.Entity<Sensor>().ToTable("Sensor");
            modelBuilder.Entity<SensorLog>().ToTable("SensorLog");
            modelBuilder.Entity<Site>().ToTable("Site");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}