using FPT_Ebook.Models;

namespace FPT_Ebook.Sessions
{
    public interface IAccountServices
    {
        public User Login (string username, string password);

    }
}
