namespace TMS2.DAL.Models;

public class Sensor
{
    public int id { get; set; }
    public int siteId { get; set; }
    public DateTime time { get; set; }
    public int sensorValue { get; set; }
    public Boolean isDefective { get; set; }
}