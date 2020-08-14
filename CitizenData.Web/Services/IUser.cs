using CitizenData.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenData.Web.Services
{
    interface IUser
    {
        IEnumerable<User> GetAll();
        User GetById(int userId);
        void Add(User newUser);
        void Update(int userId, User inputUser);
        void Delete(int userId);

    }
}
