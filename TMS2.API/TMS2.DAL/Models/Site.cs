namespace TMS2.DAL.Models;

public class Site
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string SiteManager { get; set; }
    public int SiteManagerNbr { get; set; }
    public int SensorDepth { get; set; }
    public int DrainageDepth { get; set; }
    public List<Sensor>? Sensors { get; set; }
    public List<Pump>? Pumps { get; set; }
}