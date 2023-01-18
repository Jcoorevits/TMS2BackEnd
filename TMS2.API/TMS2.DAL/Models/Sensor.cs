using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class Sensor
{
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Site")]
    public long? SiteId { get; set; }
    public long? SensorValueId { get; set; }
    public bool IsDefective { get; set; }
    public virtual ICollection<SensorLog>? SensorLogs { get; set; }
}