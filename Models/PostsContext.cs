using Microsoft.EntityFrameworkCore;

namespace PasechnikovaPR33p18.Models
{
    public class PostsContext :DbContext
    {
        public PostsContext (DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
