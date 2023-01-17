using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class PumpLog
{
    public long Id { get; set; }
    [ForeignKey("Pump")] public long PumpId { get; set; }
    [ForeignKey("User")] public long? UserId { get; set; }
    public DateTime Date { get; set; }
    public int OutputValue { get; set; }
    public int InputValue { get; set; }
    public bool IsDefective { get; set; }
}