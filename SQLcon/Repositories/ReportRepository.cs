using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository()
        {
            _context = new ApplicationDbContext();
        }

        IEnumerable<BooksView> GetBooksFromT()
        {
            return _context.BooksViews.ToList();
        }
         IEnumerable<UsersView> GetUsersFromT()
        {
            return _context.UsersViews.ToList();
        }
         IEnumerable<RentalView> GetRentalsFromT()
        {
            return _context.RentalViews.ToList();
        }
         IEnumerable<RentalView> GetRentalsByUser(int userdID)
        {
            List<RentalView> result = _context.RentalViews.FromSqlRaw($"CALL GetBooksByUserID({userdID})").ToList();
            //return _context.RentalViews.ToList();
            return result;
        }
        public string GetBooksJson()
        {
            return JsonConvert.SerializeObject(GetBooksFromT(), Formatting.Indented);
        }
        public string GetUsersJson()
        {
            return JsonConvert.SerializeObject(GetUsersFromT(), Formatting.Indented);
        }
        public string GetRentalsJson()
        {
            return JsonConvert.SerializeObject(GetRentalsFromT(), Formatting.Indented);
        }
        public string GetRentalsByUserJson(int userdID)
        {
            return JsonConvert.SerializeObject(GetRentalsByUser(userdID), Formatting.Indented);
        }
    }
}
