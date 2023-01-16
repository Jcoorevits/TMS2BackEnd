namespace TMS2.DAL.Models;

public class PompLog
{
    public int id { get; set; }
    public Pomp pompId { get; set; }
    public User userId { get; set; }
    public DateTime time { get; set; }
    public int outputValue { get; set; }
    public int inputValue { get; set; }
    public Boolean isDefective { get; set; }
}