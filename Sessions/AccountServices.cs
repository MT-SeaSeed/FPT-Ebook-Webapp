using FPT_Ebook.Models;
using Microsoft.AspNet.Identity;

namespace FPT_Ebook.Sessions
{
    public class AccountServices : IAccountServices
    {
        private List<User> users;
        public AccountServices()
        {
            users = new List<User>();
            {
                new User()
                {
                    UserId = 123456,
                    Email = "user1@gmail.com",
                    Password = "1234",
                    FullName = "Unknow",
                    Role = "Admin",
                };
                new User()
                {
                    UserId = 123456,
                    Email = "user2@gmail.com",
                    Password = "1234",
                    FullName = "Unknow",
                    Role = "Customer",
                };
            };
        }
        public User Login(string email, string password)
        {
            return users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
