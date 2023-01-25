using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class OldPumpLog
{
    public long Id { get; set; }
    [ForeignKey("OldPump")] public long? OldPumpId { get; set; }
    [ForeignKey("User")] public long? UserId { get; set; }
    public DateTime Date { get; set; }
    public string Error { get; set; }
    [ForeignKey("OldPumpValue")] public long? OldPumpValueId { get; set; }
    public bool IsDefective { get; set; }
}