namespace TMS2.DAL.Models;

public class Site
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string SiteManager { get; set; }
    public string SiteManagerNbr { get; set; }
    public double SensorDepth { get; set; }
    public double DrainageDepth { get; set; }
    public ICollection<Sensor>? Sensors { get; set; }
}