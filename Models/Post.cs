using System.ComponentModel.DataAnnotations;

namespace PasechnikovaPR33p18.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [StringLength(100)]
        public required string Title { get; set; }

        public required string Body { get; set; }

        public required DateOnly CreatedDate { get; set; }

        public string? ImageUrl { get; set; }

        public virtual User? User { get; set; }
        public int UserId { get; set; }
    }
}
