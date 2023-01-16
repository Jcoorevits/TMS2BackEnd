namespace TMS2.DAL.Models;

public class SensorLog
{
    public int id { get; set; }
    public Sensor sensorId { get; set; }
    public User userId { get; set; }
    public DateTime time { get; set; }
    public int SensorValue { get; set; }
    public Boolean isDefective { get; set; }
}