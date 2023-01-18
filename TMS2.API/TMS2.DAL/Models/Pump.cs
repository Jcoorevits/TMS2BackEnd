using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class Pump
{
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Site")] public long? SiteId { get; set; }
    public double? InputValue { get; set; }
    [ForeignKey("PumpValue")] public long? PumpValueId { get; set; }
    public bool IsDefective { get; set; }
    public ICollection<PumpLog>? PumpLogs { get; set; }
}