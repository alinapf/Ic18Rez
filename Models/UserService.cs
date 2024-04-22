using PasechnikovaPR33p18.Services;

namespace PasechnikovaPR33p18.Models
{
    public class UserService :IUserService
    {
        private readonly PostsContext context;
        private readonly IPasswordService passwordService;

        public UserService (PostsContext context, IPasswordService passwordService)
        {
            this.context = context;
            this.passwordService = passwordService;
        }

        public bool CheckPassword (string username, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user is null)
            {
                return false;
            }
            return passwordService.IsValid(password, user.Salt, user.Password);
        }

        public User? GetUser (string username) =>
            context.Users.FirstOrDefault(u => u.Username == username);

        public void Registration (string username, string password)
        {
            string salt = DateTime.UtcNow.ToString( );
            string hash = passwordService.GetHash(password, salt);
            context.Users.Add(new User
            {
                Username = username,
                Password = hash,
                Salt = salt
            });
            context.SaveChanges( );
        }
    }
}
