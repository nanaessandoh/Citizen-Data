using CitizenData.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace CitizenData.Web.Services
{
    public interface IUser
    {
        IEnumerable<User> GetAll();
        User GetById(int? userId);
        void AddUser(User newUser);
        void UpdateUser(User editUser);
        void DeleteUser(int userId);
        bool UserExists(int id);
        bool IsImage(string filename);
        string GetImageExtension(string filename);
        void DeleteProfilePhoto(int userId);
        void UploadProfileImage(string filename, string fileExtension, IFormFile imageFile);
        void UpdateAndDeleteOldPhoto(User editUser, string newImageUrl);
    }
}
