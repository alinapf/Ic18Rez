using System.Security.Cryptography;
using System.Text;

namespace PasechnikovaPR33p18.Services
{
    public class Sha256PasswordService :IPasswordService
    {
        public string GetHash (string password, string salt)
        {
            byte [ ] data = Encoding.Default.GetBytes(password + salt);
            byte [ ] hash = SHA256.HashData(data);
            return Convert.ToBase64String(hash);
        }

        public bool IsValid (string checkPassword, string salt, string passwordHash)
        {
            string hash = GetHash(checkPassword, salt);
            return hash == passwordHash;
        }
    }
}
