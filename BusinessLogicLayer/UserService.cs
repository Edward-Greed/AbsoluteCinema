using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;


namespace BusinessLogicLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }
        public String RegisterUser(string username, string email, string password)
        {
            if (UserRepository.UserExists(username))
                return "Username already exists";

            var user = new UserModel
            {
                UserName = username,
                Email = email,
                Password = HashPassword(password),
                IsCustomer = true
            };

            UserRepository.CreateUser(user);
            return "Success";
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public UserModel Authenticate(string username, string password)
        {
            var user = UserRepository.GetByUsername(username);

            if (user == null)
                return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                password,
                user.Password
            );

            return validPassword ? user : null;
        }
    }
}
