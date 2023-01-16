namespace TMS2.DAL.Models;

public class Site
{
    public int id { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string siteManager { get; set; }
    public int siteManagerNbr { get; set; }
    public int sensorDepth { get; set; }
    public int drainageDepth { get; set; }
    
    
}