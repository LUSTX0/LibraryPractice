using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IUserService
    {
        void AddUser(string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email);
        void UpdateUser(int id, string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email);
        void DeleteUser(int id);
        void AddUserObj(User user);
        void UpdateUserObj(int id, User user);
        void AddUserObj(string user);
        void UpdateUserObj(int id, string user);
        string GetUser(int id);
    }
}
