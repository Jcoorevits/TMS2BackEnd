namespace TMS2.DAL.Models;

public class Pomp
{
    public int id { get; set; }
    public int siteId { get; set; }
    public int inputValue { get; set; }
    public int outputValue { get; set; }
    public int flowRate { get; set; }
    public DateTime time { get; set; }
    public Boolean isDefective { get; set; }
}