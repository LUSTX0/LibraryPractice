using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IRentService
    {
        void AddRent(int? userId, int? bookId, DateOnly? rentDate, DateOnly? returnDate);
        void CloseRent(int id, DateOnly? endDate);
        void AddRentObj(BooksRent rent);
        void AddRentObj(string rent);
        string GetRental(int id);
    }
}
