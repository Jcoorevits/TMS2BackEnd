using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class Sensor
{
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Site")] public long? SiteId { get; set; }

    public ICollection<SensorValue>? SensorValues { get; set; }
    public bool IsDefective { get; set; }
    public virtual ICollection<SensorLog>? SensorLogs { get; set; }
    public ICollection<Pump>? Pumps { get; set; }
    public ICollection<OldPump>? OldPumps { get; set; }
    public bool SiteChange { get; set; }
    public bool SiteDelete { get; set; }
    public int Calibration { get; set; }
}