using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public interface IRentRepository
    {
        BooksRent GetRentById(int id);
        void AddRent(BooksRent rent);
        void CloseRent(BooksRent rent, DateOnly? endDate);
        BooksRent CreateRent(int? userId, int? bookId, DateOnly? rentDate, DateOnly? returnDate);

    }
}
