using System;
using System.Collections.Generic;

namespace SQLcon.Models;

public partial class UsersView
{
    public int IdUsers { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? MidName { get; set; }

    public short? YearOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }
}
