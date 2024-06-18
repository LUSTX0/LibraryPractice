using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public interface IReportRepository
    {
        string GetBooksJson();
        string GetUsersJson();
        string GetRentalsJson();
        string GetRentalsByUserJson(int userdID);

    }
}
