using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon.Repositories;
using System.Net;
using System.Text.Json;

namespace Logic.Services
{
    public class RentService : IRentService
    {       
        private readonly IRentRepository _rentRep;
        public RentService()
        {            
            _rentRep = new RentRepository();
        }

        public void AddRent(int? userId, int? bookId, DateOnly? rentDate, DateOnly? returnDate)
        {
            _rentRep.AddRent(_rentRep.CreateRent( userId,  bookId,  rentDate,  returnDate));           
        }

        public void AddRentObj(BooksRent rent)
        {
            if (rent != null) 
            { 
                _rentRep.AddRent(rent);
            }
        }

        public void AddRentObj(string rent)
        {
            if (rent != null)
            {
                BooksRent rentD = JsonSerializer.Deserialize<BooksRent>(rent);
                AddRentObj(rentD);                
            }
        }
        public void CloseRent(int id, DateOnly? endDate)
        {
           _rentRep.CloseRent(_rentRep.GetRentById(id),endDate);
        }        
    }
}
