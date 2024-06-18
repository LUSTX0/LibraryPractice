using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon.Repositories;

namespace Logic.Services
{
    public class UserService : IUserService
    {
        
        private readonly IUserRepository _uRep;
        public UserService()
        {
            _uRep = new UserRepository();
        }

        public void AddUser(string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email)
        {
            _uRep.AddUser(_uRep.CreateUser(name,midName,surname,yearOfBirth,address,email));
        }

        public void UpdateUser(int id, string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email)
        {
            _uRep.UpdateUser(id,name,midName,surname, yearOfBirth,address,email);
        }

        public void DeleteUser(int id)
        {
           _uRep.DeleteUser(id);
        }
        
    }
}
