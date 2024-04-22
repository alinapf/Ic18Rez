using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasechnikovaPR33p18.Models;
using PasechnikovaPR33p18.Services;
using System.Security.Claims;

namespace PasechnikovaPR33p18.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<Post> Posts { get; set; } = new( );
        public void OnGet()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Posts = postsService.GetPostsByUser(userId);
        }
        private readonly IPostsService postsService;

        public IndexModel (IPostsService postsService)
        {
            this.postsService = postsService;
        }
    }
}
