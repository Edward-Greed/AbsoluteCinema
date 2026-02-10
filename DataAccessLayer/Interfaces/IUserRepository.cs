using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        bool UserExists(string username);
        UserModel GetByUsername(string username);
        bool EmailExists(string email);
        void CreateUser(UserModel user);
    }
}
