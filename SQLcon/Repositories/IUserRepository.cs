using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories {
    public interface IUserRepository
    {
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(int id, string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email);
        void DeleteUser(int id);
        User CreateUser(string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email);
        void UpdateUserObj(int id, User user);
        string GetUserJson(int id);
    }
}
