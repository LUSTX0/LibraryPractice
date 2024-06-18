using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IReportService
    {
        string GetBooksJson();

        string GetUsersJson();

        string GetRentalsJson();

        string GetRentalsByUserJson(int userdID);
        
    }
}
