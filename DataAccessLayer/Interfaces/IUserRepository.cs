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
        UserModel GetByEmail(string email);
        void CreateUser(UserModel user);
    }
}
