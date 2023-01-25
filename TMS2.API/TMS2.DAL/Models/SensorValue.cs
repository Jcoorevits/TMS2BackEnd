using System.ComponentModel.DataAnnotations.Schema;

namespace TMS2.DAL.Models;

public class SensorValue
{
    public long Id { get; set; }
    [ForeignKey("Sensor")] public long SensorId { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public double? Average { get; set; }

}