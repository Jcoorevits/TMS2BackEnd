namespace TMS2.DAL.Models;

public class PumpLog
{
    public int id { get; set; }
    public Pump pompId { get; set; }
    public User userId { get; set; }
    public DateTime time { get; set; }
    public int outputValue { get; set; }
    public int inputValue { get; set; }
    public Boolean isDefective { get; set; }
}