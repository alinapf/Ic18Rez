using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasechnikovaPR33p18.Services;
using PasechnikovaPR33p18.ViewModels;

namespace PasechnikovaPR33p18.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly IUserService userService;
        private readonly INotifyService notifyService;

        public UserViewModel UserViewModel { get; set; } = new( );

        public RegistrationModel (IUserService userService, INotifyService notifyService)
        {
            this.userService = userService;
            this.notifyService = notifyService;

        }
        public IActionResult OnPost (UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page( );
            }

            var contains = userService.GetUser(userViewModel.Username);
            if (contains is not null)
            {
                ModelState.AddModelError("Username", "������������ � ����� ������ ��� ����������");
                return Page( );
            }

            try
            {
                userService.Registration(userViewModel.Username, userViewModel.Password);
                notifyService.Success("������������ ������� ���������������");
                return RedirectToPage("/Index");
            }
            catch
            {
                notifyService.Error("��������� ������ ��� ����������. ���������� ����� ��� ���������� � ��������������");
                return Page( );
            }
        }

        public void OnGet ()
        {
            // supports GET
        }
    }
}
