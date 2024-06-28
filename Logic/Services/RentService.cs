using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon.Repositories;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

namespace Logic.Services
{
    public class RentService : IRentService
    {
        private readonly IRepository<BooksRent> _rentRepTest;
        public RentService(IRepository<BooksRent> rentRepTest)
        {
            _rentRepTest = rentRepTest;
        }        
        
        //public void AddRent(int? userId, int? bookId, DateOnly? rentDate, DateOnly? returnDate)
        //{
        //    _rentRep.AddRent(_rentRep.CreateRent( userId,  bookId,  rentDate,  returnDate));           
        //}

        public void AddRentObj(BooksRent rent)
        {
            _rentRepTest.Insert(rent);
        }

        public string GetRental(int id)
        {
            return JsonConvert.SerializeObject(_rentRepTest.GetById(id), Formatting.Indented);            
        }

        public bool CloseRent(int id, DateOnly? endDate)
        {
            BooksRent currentRent = _rentRepTest.GetById(id);
            if (currentRent == null) 
            {
                return false;
            }
            
            currentRent.ReturnDate = endDate;
            _rentRepTest.Update(currentRent, id);
            return true;
        }        
    }
}
