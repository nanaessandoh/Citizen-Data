using CitizenData.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenData.Web.Services
{
    public class UserService : IUser
    {
        private readonly CitizenDataDBContext _context;
        public UserService(CitizenDataDBContext context) => _context = context;

        public void Add(User newUser)
        {
            _context.Add(newUser);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var User = GetAll().FirstOrDefault(asset => asset.Id == userId);
            if (User != null)
            {
                _context.Remove(User);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int userId)
        {
            return GetAll().FirstOrDefault(asset => asset.Id == userId);
        }

        public void Update(int userId, User inputUser)
        {
            var User =  GetAll().FirstOrDefault(asset => asset.Id == userId);

            // Given Name
            if (User.GivenName != inputUser.GivenName)
            {
                User.GivenName = inputUser.GivenName;
            }

            // Surname
            if (User.Surname != inputUser.Surname)
            {
                User.Surname = inputUser.Surname;
            }

            // Age
            if (User.Age != inputUser.Age)
            {
                User.Age = inputUser.Age;
            }

            if (User.GivenName != inputUser.GivenName)
            {
                User.GivenName = inputUser.GivenName;
            }

            if (User.GivenName != inputUser.GivenName)
            {
                User.GivenName = inputUser.GivenName;
            }


            if (User.GivenName != inputUser.GivenName)
            {
                User.GivenName = inputUser.GivenName;
            }

        }
    }
}
