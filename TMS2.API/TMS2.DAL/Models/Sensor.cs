namespace TMS2.DAL.Models;

public class Sensor
{
    public long Id { get; set; }
    public Site? Site { get; set; }
    public DateTime Time { get; set; }
    public float SensorValue { get; set; }
    public bool IsDefective { get; set; }
    public List<SensorLog>? SensorLogs { get; set; }
}