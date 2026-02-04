using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly AbsoluteCinemaDbContext UserDbContext;
        public UserRepository (AbsoluteCinemaDbContext UserDbContext)
        {
            this.UserDbContext = UserDbContext;
        }

       

        public UserModel GetByEmail(string email)
        {
            return UserDbContext.Users.FirstOrDefault(u => u.Email == email);
        }
        public bool UserExists(string username)
        {
            return UserDbContext.Users.Any(u => u.UserName == username);
        }

        public void CreateUser(UserModel user)
        {
            UserDbContext.Users.Add(user);
            UserDbContext.SaveChanges();
        }
        public UserModel GetByUsername(string username)
        {
            return UserDbContext.Users
                .FirstOrDefault(u => u.UserName == username);
        }

    }
}
