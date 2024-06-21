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
        
        private readonly IReportRepository _reportRep;
        public ReportService(IReportRepository reportRep)
        {
            _reportRep = reportRep;
        }
        
        public string GetBooksJson()
        {
            return _reportRep.GetBooksJson();
        }
        public string GetUsersJson()
        {
            return _reportRep.GetUsersJson();
        }
        public string GetRentalsJson()
        {
            return _reportRep.GetRentalsJson();
        }
        public string GetRentalsByUserJson(int userdID)
        {
            return  _reportRep.GetRentalsByUserJson(userdID);
        }
    }
}
