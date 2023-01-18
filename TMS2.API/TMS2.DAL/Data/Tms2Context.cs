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
        public DbSet<PumpValue> PumpValues { get; set; }
        public DbSet<PumpLog> PumpLogs { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorValue> SensorValues { get; set; }
        public DbSet<SensorLog> SensorLogs { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<User> Users { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder context)
        {
            context.UseSqlServer(
                "Server=tcp:hooyberghs.database.windows.net,1433;Initial Catalog=hooyberghs-db;Persist Security Info=False;User ID=hooyberghs-admin;Password=H00yberghs!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pump>().ToTable("Pump");
            modelBuilder.Entity<PumpValue>().ToTable("PumpValue");
            modelBuilder.Entity<PumpLog>().ToTable("PumpLog");
            modelBuilder.Entity<Sensor>().ToTable("Sensor");
            modelBuilder.Entity<SensorValue>().ToTable("SensorValue");
            modelBuilder.Entity<SensorLog>().ToTable("SensorLog");
            modelBuilder.Entity<Site>().ToTable("Site");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Pump>().HasData(
                new Pump {Id = 1, Name = "Pump 1", SiteId = 1, InputValue = 1, IsDefective = false},
                new Pump {Id = 2, Name = "Pump 2", SiteId = 1, InputValue = 1.0, IsDefective = false},
                new Pump {Id = 3, Name = "Pump 3", InputValue = 1.0, IsDefective = false},
                new Pump {Id = 4, Name = "Pump 4", InputValue = 1.0, IsDefective = false});
            modelBuilder.Entity<Sensor>().HasData(
                new Sensor {Id = 1, Name = "Sensor 1", SiteId = 1, IsDefective = false},
                new Sensor {Id = 2, Name = "Sensor 2", SiteId = 1, IsDefective = false},
                new Sensor {Id = 3, Name = "Sensor 3", IsDefective = false},
                new Sensor {Id = 4, Name = "Sensor 4", IsDefective = false});

            modelBuilder.Entity<Site>().HasData(new Site
            {
                Id = 1, Name = "Test Site", Address = "Antwerpen", SiteManager = "Frederik Mostmans",
                SiteManagerNbr = "0123456789", SensorDepth = 10.5,
                DrainageDepth = 8.0
            });
            modelBuilder.Entity<SensorValue>().HasData(new SensorValue
            {
                Id = 1, SensorId = 1, Value = 20.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 2, SensorId = 1, Value = 25.0, Date = DateTime.Now
            });
            modelBuilder.Entity<PumpValue>().HasData(new PumpValue
            {
                Id = 1, PumpId = 1, Value = 20.0, FlowRate = 120.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 2, PumpId = 1, Value = 25.0, FlowRate = 120.00, Date = DateTime.Now
            });
            modelBuilder.Entity<SensorLog>().HasData(new SensorLog
            {
                Id = 1, SensorId = 1, SensorValueId = 1, Date = DateTime.Now, IsDefective = true
            });
        }
    }
}