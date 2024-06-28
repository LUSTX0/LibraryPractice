using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon.Repositories;

namespace Logic.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<BooksView> _bvReportTest;
        private readonly IRepository<UsersView> _uvReportTest;
        private readonly IRepository<RentalView> _rvReportTest;
        private readonly IRepository<User> _uRepTest;

        public ReportService( IRepository<BooksView> bvRepTest, IRepository<UsersView> uvReportTest, IRepository<RentalView> rvReportTest, IRepository<User> uRepTest)
        {
            _uvReportTest = uvReportTest;
            _bvReportTest = bvRepTest;
            _rvReportTest = rvReportTest;
            _uRepTest = uRepTest;
        }
        
        public string GetBooksJson()
        {
            return JsonConvert.SerializeObject(_bvReportTest.GetAll(), Formatting.Indented);           
        }
        public string GetUsersJson()
        {
            return JsonConvert.SerializeObject(_uvReportTest.GetAll(), Formatting.Indented);
        }
        public string GetRentalsJson()
        {
            return JsonConvert.SerializeObject(_rvReportTest.GetAll(), Formatting.Indented);
        }
        public string GetRentalsByUserJson(int userdID)
        { 
            User currentUser = _uRepTest.GetById(userdID);
            if (currentUser != null)
            {
                return JsonConvert.SerializeObject(_rvReportTest.GetAll(a => a.Surname == currentUser.Surname && a.MidName == currentUser.MidName && a.Name == currentUser.Name), Formatting.Indented);
            }
            else
            {
                return "null";
            }
        }
    }
}
