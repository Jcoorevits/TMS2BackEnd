using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class OldPump
{
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Site")] public long? SensorId { get; set; }
    public bool InputValue { get; set; }
    public ICollection<OldPumpValue>? OldPumpValues { get; set; }
    public bool IsDefective { get; set; }
    public ICollection<OldPumpLog>? PumpLogs { get; set; }
    public bool IsUserInput { get; set; }
    public bool Repair { get; set; }
    public bool SiteChange { get; set; }
    public bool SiteDelete { get; set; }
    public int Calibration { get; set; }
    public string? User { get; set; }
}