using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasechnikovaPR33p18.Services;
using PasechnikovaPR33p18.ViewModels;
using System.Security.Claims;

namespace PasechnikovaPR33p18.Pages
{
    public class UpdatePostModel : PageModel
    {
        private readonly IPostsService postsService;
        private readonly INotifyService notifyService;

        public UpdatePostViewModel PostViewModel { get; set; } = new();
        public UpdatePostModel(IPostsService postsService, INotifyService notifyService)
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
        public async Task<IActionResult> OnPost(UpdatePostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = postsService.FindPost(postViewModel.PostId);
            if (post is null)
            {
                ModelState.AddModelError("", "Задан некорректный идентификатор записи");
                return Page();
            }

            if (postViewModel.Image is not null)
            {
                string ext = Path.GetExtension(postViewModel.Image.FileName);
                string filename = Path.GetRandomFileName() + ext;
                string filePath = Path.Combine(@"wwwroot/images", filename);
                using (FileStream file = new(filePath, FileMode.Create))
                {
                    await postViewModel.Image.CopyToAsync(file);
                }
                post.ImageUrl = filename;
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (post.UserId != userId)
            {
                return Forbid();
            }

            post.Title = postViewModel.Title;
            post.Body = postViewModel.Body;
            try
            {
                postsService.UpdatePost(post.PostId, post);
                notifyService.Success("Запись успешно изменена");
            }
            catch
            {
                notifyService.Error("Произошла ошибка при изменении записи");
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
