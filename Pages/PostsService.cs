using Microsoft.EntityFrameworkCore;
using PasechnikovaPR33p18.Models;
using PasechnikovaPR33p18.Services;

namespace PasechnikovaPR33p18.Pages
{
    public class PostsService :IPostsService
    {
        private readonly PostsContext context;

        public PostsService (PostsContext context)
        {
            this.context = context;
        }

        public void AddPost (Post post)
        {
            context.Entry(post).State = EntityState.Added;
            context.SaveChanges( );
        }

        public void DeletePost (Post post)
        {
            context.Entry(post).State = EntityState.Deleted;
            context.SaveChanges( );
        }

        public Post? FindPost (int postId)
        {
            return context.Posts.Find(postId);
        }

        public List<Post> GetPostsByUser (int userId)
        {
            return context.Posts.Where(p => p.UserId == userId).ToList( );
        }

        public void UpdatePost (int postId, Post post)
        {
            var toUpdate = context.Posts.Find(postId);
            if (toUpdate is null)
            {
                throw new ArgumentException("Invalid post id");
            }

            toUpdate.Title = post.Title;
            toUpdate.ImageUrl = post.ImageUrl;
            toUpdate.Body = post.Body;
            toUpdate.CreatedDate = DateOnly.FromDateTime(DateTime.Today);

            context.SaveChanges( );
        }
    }
}
