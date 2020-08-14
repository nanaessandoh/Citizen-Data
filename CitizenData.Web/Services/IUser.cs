using CitizenData.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        bool IsImage(string Filename);
        string GetImageExtension(string Filename);
    }
}
