using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon;
using SQLcon.Models;
namespace SQLcon.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            if (_context.Books.Find(id) == null)
            {
                throw new ArgumentException("There is no such book", nameof(id));
            }
            else
            {
                return _context.Books.Find(id);
            }
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(int id, string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate)
        {
            var existingBook = GetBookById(id);
            if (existingBook != null)
            {
                if (title != null)
                {
                    existingBook.Title = title;
                }
                if (receiptDate != null)
                {
                    existingBook.ReceiptDate =receiptDate;
                }
                if (writeOffDate != null)
                {
                    existingBook.WriteOffDate =writeOffDate;
                }
                if (author != null)
                {
                    existingBook.Author =author;
                }
                if (inventoryNumber != null)
                {
                    existingBook.InventoryNumber =inventoryNumber;
                }
                if (year != null)
                {
                    existingBook.ReleaseYear = year;
                }

                _context.SaveChanges();
            }
            else
            {
                // Обработка ситуации, когда объект не найден
                Console.WriteLine("Book not found.");
            }
        }

        public void DeleteBook(int id)
        {
            var product = GetBookById(id);
            if (product != null)
            {
                _context.Books.Remove(product);
                _context.SaveChanges();
            }
        }
        public void WriteOFFBook(int id, DateOnly date)
        {
            var existingBook = GetBookById(id);
            if (existingBook != null)
            {
                existingBook.WriteOffDate = date;
                _context.SaveChanges();
            }
        }
        public Book CreateBook(string? title, string? author,string? inventoryNumber,short? year, DateOnly? receiptDate,DateOnly? writeOffDate)
        {
            Book existingBook = new Book();
            existingBook.Title = title;
            existingBook.ReceiptDate = receiptDate;
            existingBook.WriteOffDate = writeOffDate;
            existingBook.Author = author;
            existingBook.InventoryNumber = inventoryNumber;
            existingBook.ReleaseYear = year;
            return existingBook;
        }

        public void UpdateBookObj(int id, Book book)
        {
            var existingBook = GetBookById(id);
            if (existingBook != null)
            {
                if (book.Title != null)
                {
                    existingBook.Title = book.Title;
                }
                if (book.ReceiptDate != null)
                {
                    existingBook.ReceiptDate = book.ReceiptDate;
                }
                if (book.WriteOffDate != null)
                {
                    existingBook.WriteOffDate = book.WriteOffDate;
                }
                if (book.Author != null)
                {
                    existingBook.Author = book.Author;
                }
                if (book.InventoryNumber != null)
                {
                    existingBook.InventoryNumber = book.InventoryNumber;
                }
                if (book.ReleaseYear != null)
                {
                    existingBook.ReleaseYear = book.ReleaseYear;
                }

                _context.SaveChanges();
            }
            else
            {
                // Обработка ситуации, когда объект не найден
                Console.WriteLine("Book not found.");
            }
        }
    }
}
