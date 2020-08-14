﻿using CitizenData.Web.Models;
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

        public void AddUser(User newUser)
        {
            _context.Add(newUser);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
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

        public User GetById(int? userId)
        {
            return GetAll().FirstOrDefault(asset => asset.Id == userId);
        }

        public bool IsImage(string Filename)
        {
            bool isPNG = Filename.Contains(".png", StringComparison.CurrentCultureIgnoreCase);
            bool isJPG = Filename.Contains(".jpg", StringComparison.CurrentCultureIgnoreCase);
            bool isJPEG = Filename.Contains(".jpeg", StringComparison.CurrentCultureIgnoreCase);
            bool isBMP = Filename.Contains(".bmp", StringComparison.CurrentCultureIgnoreCase);

            return isBMP || isJPEG || isJPG || isPNG;
        }

        public void UpdateUser(User editUser)
        {
            _context.Update(editUser);
            _context.SaveChanges();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(asset => asset.Id == id);
        }

    }
}
