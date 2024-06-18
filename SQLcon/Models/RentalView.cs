using System;
using System.Collections.Generic;

namespace SQLcon.Models;

public partial class RentalView
{
    public string? Name { get; set; }

    public string? MidName { get; set; }

    public string? Surname { get; set; }

    public string? BookTitle { get; set; }

    public string? Author { get; set; }

    public DateOnly? RentDate { get; set; }

    public DateOnly? ReturnDate { get; set; }
}
