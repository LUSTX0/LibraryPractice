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
using Newtonsoft.Json;

namespace Logic.Services
{
    public class UserService : IUserService
    {        
        
        private readonly IRepository<User> _uRepTest;
        public UserService( IRepository<User> uRepTest)
        {            
            _uRepTest = uRepTest;
        }

        //public void AddUser(string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email)
        //{
        //    _uRep.AddUser(_uRep.CreateUser(name,midName,surname,yearOfBirth,address,email));
        //}

        //public void UpdateUser(int id, string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email)
        //{
        //    _uRep.UpdateUser(id,name,midName,surname, yearOfBirth,address,email);
        //}

        public void DeleteUser(int id)
        {
            _uRepTest.Delete(id);
        }
        public string GetUser(int id)
        {
            return JsonConvert.SerializeObject(_uRepTest.GetById(id), Formatting.Indented);            
        }
        public void AddUserObj(User user)
        {
            if (user != null)
            {
                _uRepTest.Insert(user);
            }
        }

        public void UpdateUserObj(int id, User user)
        {
            if (user != null)
            {
                _uRepTest.Update(user,id);
            }            
        }

    }
}
