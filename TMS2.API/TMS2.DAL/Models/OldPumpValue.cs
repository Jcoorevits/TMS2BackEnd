using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class OldPumpValue
{
    public long Id { get; set; }
    [ForeignKey("OldPump")] public long OldPumpId { get; set; }
    public double Value { get; set; }
    public double FlowRate { get; set; }
    public DateTime Date { get; set; }
}