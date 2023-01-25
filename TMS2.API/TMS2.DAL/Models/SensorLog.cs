using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class SensorLog
{
    public long Id { get; set; }
    [ForeignKey("Sensor")] public long SensorId { get; set; }
    public DateTime Date { get; set; }
    [ForeignKey("SensorValue")] public long SensorValueId { get; set; }
    public string Error { get; set; }
    public bool IsDefective { get; set; }

}