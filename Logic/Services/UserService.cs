using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLcon.Repositories;
using System.Net;
using System.Xml.Linq;
using System.Text.Json;

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

        public void AddUserObj(User user)
        {
            if (user != null)
            {
                _uRep.AddUser(user);
            }
        }

        public void UpdateUserObj(int id, User user)
        {
            if (user != null)
            {
                _uRep.UpdateUserObj(id, user);
            }            
        }

        public void AddUserObj(string user)
        {            
            if (user != null)
            {
                User userD = JsonSerializer.Deserialize<User>(user);
                AddUserObj(userD);                         
            }
        }

        public void UpdateUserObj(int id, string user)
        {
            if (user != null)
            {
                User userD = JsonSerializer.Deserialize<User>(user);
                UpdateUserObj(id,userD);                
            }            
        }
    }
}
