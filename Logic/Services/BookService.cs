using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon;
using SQLcon.Models;
using SQLcon.Repositories;
using System.Text.Json;
using Newtonsoft.Json;

namespace Logic.Services
{
    public class BookService : IBookService
    {
       
        private readonly IRepository<Book> _bRepTest;
        public BookService( IRepository<Book> bRepTest)
        {            
            _bRepTest = bRepTest;
        }
        
        
        //public void AddBook(string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate)
        //{
        //    _bRep.AddBook(_bRep.CreateBook(title,author,inventoryNumber,year,receiptDate,writeOffDate));
        //}
        
        //public void UpdateBook(int id, string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate)
        //{
        //    _bRep.UpdateBook(id,  title,  author,  inventoryNumber,  year,  receiptDate,  writeOffDate);
            
        //}

        public string GetBook(int id)
        {            
            return JsonConvert.SerializeObject(_bRepTest.GetById(id), Formatting.Indented);
        }

        public bool DeleteBook(int id)
        {
            Book currentBook = _bRepTest.GetById(id);
            if (currentBook == null)
            {
                return false;
            }

            _bRepTest.Delete(id);
            return true;
        }

        public bool WriteOFFBook(int id, DateOnly date)
        {
            Book currentBook = _bRepTest.GetById(id);
            if (currentBook == null)
            {
                return false;
            }
            currentBook.WriteOffDate = date;
            UpdateBookObj(id, currentBook); 
            return true;
        }

        public void AddBookObj(Book book)
        {
            _bRepTest.Insert(book);            
        }

        public bool UpdateBookObj(int id, Book book)
        {
            Book currentBook = _bRepTest.GetById(id);
            if (currentBook == null)
            {
                return false;
            }

            _bRepTest.Update(book, id);
            return true;
        }
        
    }
}
