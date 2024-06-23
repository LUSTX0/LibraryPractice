using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        void UpdateBook(int id, string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate);
        void DeleteBook(int id);
        void WriteOFFBook(int id, DateOnly date);
        Book CreateBook(string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate);
        Book GetBookById(int id);
        void UpdateBookObj(int id, Book book);
        string GetBookJson(int id);

    }
}
