using CitizenData.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        public bool IsImage(string filename)
        {
            bool isPNG = filename.Contains(".png", StringComparison.CurrentCultureIgnoreCase);
            bool isJPG = filename.Contains(".jpg", StringComparison.CurrentCultureIgnoreCase);
            bool isJPEG = filename.Contains(".jpeg", StringComparison.CurrentCultureIgnoreCase);
            bool isBMP = filename.Contains(".bmp", StringComparison.CurrentCultureIgnoreCase);

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

        public string GetImageExtension(string filename)
        {
            if (filename.Contains(".png", StringComparison.CurrentCultureIgnoreCase)) return ".png";
            if (filename.Contains(".jpeg", StringComparison.CurrentCultureIgnoreCase)) return ".jpep";
            if (filename.Contains(".jpg", StringComparison.CurrentCultureIgnoreCase)) return ".jpg";
            if (filename.Contains(".bmp", StringComparison.CurrentCultureIgnoreCase)) return ".bmp";
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

        public void UploadProfileImage(string filename, string fileExtension, IFormFile imageFile)
        {
            // Create path for new file
            string filePath = $"{_env.WebRootPath}\\images\\{filename}{fileExtension}";
            using (FileStream stream = System.IO.File.Create(filePath))
            {
                imageFile.CopyTo(stream);
                stream.Flush();
            }
        }

        public void UpdateAndDeleteOldPhoto(User editUser, string newImageUrl)
        {
            // Select current ImageUrl
            string filename = editUser.ImageUrl;

            try
            {
                // Construct the relative path of the file
                string filePath = $"{_env.WebRootPath}{filename}";
                // Delete file from wwwroot folder
                File.Delete(filePath);
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }

            //Update to new ImageUrl
            editUser.ImageUrl = newImageUrl;
            _context.Update(editUser);
            _context.SaveChanges();
        }

    }
}
