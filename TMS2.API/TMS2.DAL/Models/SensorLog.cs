namespace TMS2.DAL.Models;

public class SensorLog
{
    public long Id { get; set; }
    public Sensor? SensorId { get; set; }
    public DateTime Time { get; set; }
    public int SensorValue { get; set; }
    public bool IsDefective { get; set; }
}