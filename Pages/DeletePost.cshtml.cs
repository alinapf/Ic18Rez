using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasechnikovaPR33p18.Services;
using PasechnikovaPR33p18.ViewModels;
using System.Security.Claims;

namespace PasechnikovaPR33p18.Pages
{
    public class DeletePostModel : PageModel
    {
        private readonly IPostsService postsService;
        private readonly INotifyService notifyService;
        public DeletePostViewModel PostViewModel { get; set; } = new();
        public DeletePostModel(IPostsService postsService, INotifyService notifyService)
        {
            this.postsService = postsService;
            this.notifyService = notifyService;
        }
        public IActionResult OnGet(int postId)
        {
            var post = postsService.FindPost(postId);
            if (post is null || post.UserId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return NotFound();
            }
            PostViewModel.PostId = post.PostId;
            PostViewModel.Title = post.Title;
            PostViewModel.Body = post.Body;
            PostViewModel.CurrentImage = post.ImageUrl;
            return Page();
        }
        public IActionResult OnPost(DeletePostViewModel postViewModel)
        {

            var post = postsService.FindPost(postViewModel.PostId);


            try
            {
                postsService.DeletePost(post);
                notifyService.Success("Запись успешно удалена");
            }
            catch
            {
                notifyService.Error("Произошла ошибка при удалении записи");
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
