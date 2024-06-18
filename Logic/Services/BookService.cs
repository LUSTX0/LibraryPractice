using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon;
using SQLcon.Models;
using SQLcon.Repositories;

namespace Logic.Services
{
    public class BookService : IBookService
    {
        //private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bRep;
        public BookService()
        {
            _bRep = new BookRepository();
        }

        public void AddBook(string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate)
        {
            _bRep.AddBook(_bRep.CreateBook(title,author,inventoryNumber,year,receiptDate,writeOffDate));
        }

        public void UpdateBook(int id, string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate)
        {
            _bRep.UpdateBook(id,  title,  author,  inventoryNumber,  year,  receiptDate,  writeOffDate);
            
        }

        public void DeleteBook(int id)
        {
            _bRep.DeleteBook(id);
        }

        public void WriteOFFBook(int id, DateOnly date)
        {
           _bRep.WriteOFFBook(id, date);
        }
    }
}
