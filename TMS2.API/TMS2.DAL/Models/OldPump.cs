using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class OldPump
{
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Site")] public long? SiteId { get; set; }
    public int InputValue { get; set; }
    public ICollection<OldPumpValue>? OldPumpValues { get; set; }
    public bool IsDefective { get; set; }
    public ICollection<PumpLog>? PumpLogs { get; set; }
}