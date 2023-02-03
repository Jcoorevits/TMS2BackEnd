using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class Pump
{
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Sensor")] public long? SensorId { get; set; }
    public double InputValue { get; set; }
    public ICollection<PumpValue>? PumpValues { get; set; }
    public bool IsDefective { get; set; }
    public ICollection<PumpLog>? PumpLogs { get; set; }
    public bool IsUserInput { get; set; }
    public bool Repair { get; set; }
    public bool SiteChange { get; set; }
    public bool SiteDelete { get; set; }
    public int Calibration { get; set; }
    public bool TawReached { get; set; }
    public string? User { get; set; }
}