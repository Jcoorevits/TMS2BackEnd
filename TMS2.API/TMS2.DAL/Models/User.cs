namespace TMS2.DAL.Models;

public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public int phone { get; set; }
    public string email { get; set; }
    public Boolean isAdmin { get; set; }
}