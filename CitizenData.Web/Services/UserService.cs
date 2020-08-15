using CitizenData.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenData.Web.Services
{
    public class UserService : IUser
    {
        private readonly CitizenDataDBContext _context;
        private readonly IWebHostEnvironment _env;
        public UserService(CitizenDataDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

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

        public string GetImageExtension(string Filename)
        {
            if (Filename.Contains(".png", StringComparison.CurrentCultureIgnoreCase)) return ".png";
            if (Filename.Contains(".jpeg", StringComparison.CurrentCultureIgnoreCase)) return ".jpep";
            if (Filename.Contains(".jpg", StringComparison.CurrentCultureIgnoreCase)) return ".jpg";
            if (Filename.Contains(".bmp", StringComparison.CurrentCultureIgnoreCase)) return ".bmp";
            else return "";
        }

        public void DeleteProfilePhoto(int userId)
        {
            // Select Filename of the user
            string filename = _context.Users.FirstOrDefault(asset => asset.Id == userId).ImageUrl;

            try
            {
                // Construct the relative path of the file
                string filePath = $"{_env.WebRootPath}{filename}";
                File.Delete(filePath);
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }

        }

    }
}
