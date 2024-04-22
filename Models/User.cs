using System.ComponentModel.DataAnnotations;

namespace PasechnikovaPR33p18.Models
{
    public class User
    {
        public int UserId { get; set; }

        [StringLength(150)]
        public required string Username { get; set; }

        [StringLength(300)]
        public required string Password { get; set; }

        [StringLength(300)]
        public required string Salt { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
    }
}
