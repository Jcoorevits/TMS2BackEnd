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
        public DbSet<OldPump> OldPumps { get; set; }
        public DbSet<PumpValue> PumpValues { get; set; }
        public DbSet<OldPumpValue> OldPumpValues { get; set; }
        public DbSet<PumpLog> PumpLogs { get; set; }
        public DbSet<OldPumpLog> OldPumpLogs { get; set; }
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
            modelBuilder.Entity<OldPump>().ToTable("OldPump");
            modelBuilder.Entity<PumpValue>().ToTable("PumpValue");
            modelBuilder.Entity<OldPumpValue>().ToTable("OldPumpValue");
            modelBuilder.Entity<PumpLog>().ToTable("PumpLog");
            modelBuilder.Entity<OldPumpLog>().ToTable("OldPumpLog");
            modelBuilder.Entity<Sensor>().ToTable("Sensor");
            modelBuilder.Entity<SensorValue>().ToTable("SensorValue");
            modelBuilder.Entity<SensorLog>().ToTable("SensorLog");
            modelBuilder.Entity<Site>().ToTable("Site");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Pump>().HasData(
                new Pump {Id = 1, Name = "Pump 1", SensorId = 1, InputValue = 1.0, IsDefective = false},
                new Pump {Id = 2, Name = "Pump 2", SensorId = 1, InputValue = 1.0, IsDefective = false},
                new Pump {Id = 3, Name = "Pump 3", InputValue = 100.0, IsDefective = false},
                new Pump {Id = 4, Name = "Pump 4", InputValue = 1.0, IsDefective = false},
                new Pump {Id = 5, Name = "Pump 5", InputValue = 1.0, IsDefective = false},
                new Pump {Id = 6, Name = "Pump 6", InputValue = 1.0, IsDefective = false},
                new Pump {Id = 7, Name = "Pump 7", InputValue = 1.0, IsDefective = false},
                new Pump {Id = 8, Name = "Pump 8", InputValue = 1.0, IsDefective = false});
            modelBuilder.Entity<OldPump>().HasData(
                new OldPump {Id = 1, Name = "Old Pump 1", SensorId = 2, InputValue = true, IsDefective = false},
                new OldPump {Id = 2, Name = "Old Pump 2", SensorId = 2, InputValue = false, IsDefective = false},
                new OldPump {Id = 3, Name = "Old Pump 3", InputValue = false, IsDefective = false},
                new OldPump {Id = 4, Name = "Old Pump 4", InputValue = false, IsDefective = false});

            modelBuilder.Entity<Sensor>().HasData(
                new Sensor {Id = 1, Name = "Sensor 1", SiteId = 1, IsDefective = false, Calibration = 0},
                new Sensor {Id = 2, Name = "Sensor 2", SiteId = 1, IsDefective = false, Calibration = 0},
                new Sensor {Id = 3, Name = "Sensor 3", IsDefective = false, Calibration = 0},
                new Sensor {Id = 4, Name = "Sensor 4", IsDefective = false, Calibration = 0},
                new Sensor {Id = 5, Name = "Sensor 5", IsDefective = false, Calibration = 0},
                new Sensor {Id = 6, Name = "Sensor 6", IsDefective = false, Calibration = 0},
                new Sensor {Id = 7, Name = "Sensor 7", IsDefective = false, Calibration = 0},
                new Sensor {Id = 8, Name = "Sensor 8", IsDefective = false, Calibration = 0});

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
            }, new SensorValue
            {
                Id = 3, SensorId = 1, Value = 20.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 4, SensorId = 1, Value = 25.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 5, SensorId = 1, Value = 20.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 6, SensorId = 2, Value = 25.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 7, SensorId = 2, Value = 20.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 8, SensorId = 2, Value = 25.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 9, SensorId = 2, Value = 20.0, Date = DateTime.Now
            }, new SensorValue
            {
                Id = 10, SensorId = 2, Value = 25.0, Date = DateTime.Now
            });
            modelBuilder.Entity<PumpValue>().HasData(new PumpValue
            {
                Id = 1, PumpId = 1, Value = 20.0, FlowRate = 120.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 2, PumpId = 1, Value = 25.0, FlowRate = 100.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 3, PumpId = 1, Value = 20.0, FlowRate = 140.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 4, PumpId = 1, Value = 25.0, FlowRate = 160.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 5, PumpId = 1, Value = 20.0, FlowRate = 120.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 6, PumpId = 1, Value = 20.0, FlowRate = 130.00, Date = DateTime.Now
            }, new PumpValue
            {
                Id = 7, PumpId = 1, Value = 25.0, FlowRate = 140.00, Date = DateTime.Now
            });
            modelBuilder.Entity<OldPumpValue>().HasData(new OldPumpValue
            {
                Id = 1, OldPumpId = 1, Value = 20.0, FlowRate = 120.00, Date = DateTime.Now
            }, new OldPumpValue
            {
                Id = 2, OldPumpId = 1, Value = 25.0, FlowRate = 100.00, Date = DateTime.Now
            }, new OldPumpValue
            {
                Id = 3, OldPumpId = 1, Value = 20.0, FlowRate = 140.00, Date = DateTime.Now
            }, new OldPumpValue
            {
                Id = 4, OldPumpId = 1, Value = 25.0, FlowRate = 160.00, Date = DateTime.Now
            }, new OldPumpValue
            {
                Id = 5, OldPumpId = 1, Value = 20.0, FlowRate = 120.00, Date = DateTime.Now
            }, new OldPumpValue
            {
                Id = 6, OldPumpId = 1, Value = 20.0, FlowRate = 130.00, Date = DateTime.Now
            }, new OldPumpValue
            {
                Id = 7, OldPumpId = 1, Value = 25.0, FlowRate = 140.00, Date = DateTime.Now
            });
            modelBuilder.Entity<SensorLog>().HasData(new SensorLog
                {
                    Id = 1, SensorId = 1, Error = "Dit is een error", SensorValueId = 1, Date = DateTime.Now,
                    IsDefective = true
                },
                new SensorLog
                {
                    Id = 2, SensorId = 1, Error = "Dit is ook een error", SensorValueId = 1, Date = DateTime.Now,
                    IsDefective = true
                });
            modelBuilder.Entity<PumpLog>().HasData(new PumpLog
            {
                Id = 1, PumpId = 1, Error = "Dit is een error", PumpValueId = 1, Date = DateTime.Now, IsDefective = true
            });
            modelBuilder.Entity<OldPumpLog>().HasData(new OldPumpLog
            {
                Id = 1, OldPumpId = 1, Error = "Dit is een error", OldPumpValueId = 1, Date = DateTime.Now,
                IsDefective = true
            });
        }
    }
}