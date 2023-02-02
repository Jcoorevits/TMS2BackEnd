﻿namespace TMS2.DAL.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsVerified { get; set; }
}