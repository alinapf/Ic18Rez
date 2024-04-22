namespace PasechnikovaPR33p18.Services
{
    public interface IPasswordService
    {
        string GetHash (string password, string salt);
        bool IsValid (string checkPassword, string salt, string passwordHash);
    }
}
