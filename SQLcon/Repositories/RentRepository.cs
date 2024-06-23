using Newtonsoft.Json;
using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly ApplicationDbContext _context;

        public RentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IEnumerable<User> GetAllUsers()
        //{
        //    return _context.Users.ToList();
        //}
        public string GetRentJson(int id)
        {
            return JsonConvert.SerializeObject(GetRentById(id), Formatting.Indented);
        }
        public BooksRent GetRentById(int id)
        {
            if (_context.BooksRents.Find(id) == null)
            {
                throw new ArgumentException("There is no such rent data", nameof(id));
            }
            else
            {
                return _context.BooksRents.Find(id);
            }
        }

        public void AddRent(BooksRent rent)
        {
            _context.BooksRents.Add(rent);
            _context.SaveChanges();
        }

        public void CloseRent(BooksRent rent, DateOnly? endDate)
        {
            var existingRent = GetRentById(rent.IdBooksRent);
            if (existingRent != null)
            {
                existingRent.ReturnDate = endDate;                

                _context.SaveChanges();
            }
            else
            {
                // Обработка ситуации, когда объект не найден
                Console.WriteLine("Rent of book is not found.");
            }
        }
        public BooksRent CreateRent(int? userId, int? bookId, DateOnly? rentDate, DateOnly? returnDate)
        {
            BooksRent rentedBook = new BooksRent();
            rentedBook.UserId = userId;
            rentedBook.BookId = bookId;
            rentedBook.RentDate = rentDate;
            rentedBook.ReturnDate = returnDate;
           
            return rentedBook;
        }
        //public void DeleteUser(int id)
        //{
        //    var product = _context.Users.Find(id);
        //    if (product != null)
        //    {
        //        _context.Users.Remove(product);
        //        _context.SaveChanges();
        //    }
        //}
    }
}
