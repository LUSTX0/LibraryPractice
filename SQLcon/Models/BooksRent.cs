using System;
using System.Collections.Generic;

namespace SQLcon.Models;

/// <summary>
/// List of rented books
/// </summary>
public partial class BooksRent
{
    public int IdBooksRent { get; set; }

    public DateOnly? RentDate { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
