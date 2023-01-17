using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class PumpValue
{
    public long Id { get; set; }
    [ForeignKey("Pump")]
    public long PumpId { get; set; }
    public double Value { get; set; }
    public double FlowRate { get; set; }
    public DateTime Date { get; set; }
}