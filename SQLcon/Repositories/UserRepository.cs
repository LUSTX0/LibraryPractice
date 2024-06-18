using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            if (_context.Users.Find(id) == null)
            {
                throw new ArgumentException("There is no such user", nameof(id));
            }
            else
            {
                return _context.Users.Find(id);
            }
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(int id, string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email)
        {
            var existingUser = GetUserById(id);
            if (existingUser != null)
            {
                if (name != null)
                {
                    existingUser.Name = name;
                }
                if (midName != null)
                {
                    existingUser.MidName = midName;
                }
                if (surname != null)
                {
                    existingUser.Surname = surname;
                }
                if (yearOfBirth != null)
                {
                    existingUser.YearOfBirth = yearOfBirth;
                }
                if (address != null)
                {
                    existingUser.Address = address;
                }
                if (email != null)
                {
                    existingUser.Email = email;
                }                

                _context.SaveChanges();
            }
            else
            {
                // Обработка ситуации, когда объект не найден
                Console.WriteLine("User not found.");
            }
        }

        public void DeleteUser(int id)
        {
            var product = GetUserById(id);
            if (product != null)
            {
                _context.Users.Remove(product);
                _context.SaveChanges();
            }
        }
        public User CreateUser(string? name, string? midName, string? surname, short? yearOfBirth, string? address, string? email)
        {
            User existingUser = new User();
            existingUser.Name = name;
            existingUser.MidName = midName;
            existingUser.Surname = surname;
            existingUser.YearOfBirth = yearOfBirth;
            existingUser.Address = address;
            existingUser.Email = email;
            return existingUser;
        }
    }
}
