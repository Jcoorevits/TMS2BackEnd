namespace TMS2.DAL.Models;

public class Pump
{
    public long Id { get; set; }
    public Site? Site { get; set; }
    public int InputValue { get; set; }
    public int OutputValue { get; set; }
    public int FlowRate { get; set; }
    public DateTime Time { get; set; }
    public bool IsDefective { get; set; }
    public List<PumpLog>? PumpLogs { get; set; }
}