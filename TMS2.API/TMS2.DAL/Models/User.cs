namespace TMS2.DAL.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<PumpLog>? PumpLogs { get; set; }
}