using PasechnikovaPR33p18.Models;

namespace PasechnikovaPR33p18.Services
{
    public interface IPostsService
    {
        List<Post> GetPostsByUser (int userId);
        Post? FindPost (int postId);
        void AddPost (Post post);
        void UpdatePost (int postId, Post post);
        void DeletePost (Post post);
    }
}
