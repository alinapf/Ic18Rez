using PasechnikovaPR33p18.Models;

namespace PasechnikovaPR33p18.Services
{
    public interface IUserService
    {
        void Registration (string username, string password);
        bool CheckPassword (string username, string password);
        User? GetUser (string username);
    }
}
