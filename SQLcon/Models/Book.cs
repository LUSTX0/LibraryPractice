using System;
using System.Collections.Generic;

namespace SQLcon.Models;

/// <summary>
/// Books list
/// </summary>
public partial class Book
{
    public int IdBooks { get; set; }

    public string? InventoryNumber { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public short? ReleaseYear { get; set; }

    public DateOnly? ReceiptDate { get; set; }

    public DateOnly? WriteOffDate { get; set; }

    public virtual ICollection<BooksRent> BooksRents { get; set; } = new List<BooksRent>();
}
