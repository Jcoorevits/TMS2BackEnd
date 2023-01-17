namespace TMS2.DAL.Models;

public class PumpLog
{
    public long Id { get; set; }
    public Pump PumpId { get; set; }
    public User UserId { get; set; }
    public DateTime Time { get; set; }
    public int OutputValue { get; set; }
    public int InputValue { get; set; }
    public bool IsDefective { get; set; }
}