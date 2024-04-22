using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PasechnikovaPR33p18.ViewModels
{
    public class DeletePostViewModel
    {
        [Key]
        public int PostId { get; set; }

        [Display(Name = "Название заметки")]
        [Required(ErrorMessage = "Укажите название заметки")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Текст заметки")]
        [Required(ErrorMessage = "Текст не может быть пустым")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; } = string.Empty;

        [Display(Name = "Текущее изображение")]
        public string? CurrentImage { get; set; }

        [Display(Name = "Изменить изображение")]
        public IFormFile? Image { get; set; }
    }
}
