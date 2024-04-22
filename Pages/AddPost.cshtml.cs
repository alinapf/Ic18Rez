using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasechnikovaPR33p18.Models;
using PasechnikovaPR33p18.Services;
using PasechnikovaPR33p18.ViewModels;
using System.Security.Claims;

namespace PasechnikovaPR33p18.Pages
{
    public class AddPostModel : PageModel
    {
        private readonly IPostsService postsService;
        private readonly INotifyService notifyService;

        public AddPostViewModel AddPostViewModel { get; set; } = null!;

        public AddPostModel(IPostsService postsService, INotifyService notifyService)
        {
            this.postsService = postsService;
            this.notifyService = notifyService;
        }

        public IActionResult OnGet ()
        {
            return Page( );
        }
        public async Task<IActionResult> OnPost (AddPostViewModel addPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page( );
            }

            string? url = null;

            if (addPostViewModel.Image is not null)
            {
                string ext = Path.GetExtension(addPostViewModel.Image.FileName);
                string filename = Path.GetRandomFileName( ) + ext;
                string filePath = Path.Combine(@"wwwroot/images", filename);
                using (FileStream file = new(filePath, FileMode.Create))
                {
                    await addPostViewModel.Image.CopyToAsync(file);
                }
                url = filename;
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            Post post = new Post
            {
                Body = addPostViewModel.Body,
                Title = addPostViewModel.Title,
                CreatedDate = DateOnly.FromDateTime(DateTime.Today),
                ImageUrl = url,
                UserId = userId
            };

            try
            {
                postsService.AddPost(post);
                notifyService.Success("Запись успешно добавлена");
            }
            catch
            {
                notifyService.Error("Произошла ошибка при добавлении записи");
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
