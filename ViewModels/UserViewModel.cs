using System.ComponentModel.DataAnnotations;

namespace PasechnikovaPR33p18.ViewModels
{
    public class UserViewModel
    {
        [StringLength(30, ErrorMessage = "Имя пользователя не может содержать более 30 символов")]
        [Required(ErrorMessage = "Имя пользователя не может быть пустым")]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "Пароль не может содержать менее 8 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Повторите пароль")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
