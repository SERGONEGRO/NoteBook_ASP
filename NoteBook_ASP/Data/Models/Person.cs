using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace NoteBook_ASP.Models
{
    public class Person
    {
        //[BindNever] если активировать, то id не передается из View в контроллер, и метод Edit например не работает
        public int Id { get; set; }

        [Display(Name = "Введите имя")]
        [StringLength(25,MinimumLength = 3)]
        [Required(ErrorMessage = "Длина имени 3-25 символов")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Введите Фамилию")]
        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Длина фамилии 3-25 символов")]
        public string SurName { get; set; } = string.Empty;

        [Display(Name = "Введите Отчество")]
        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Длина отчества 3-25 символов")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Введите номер")]
        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Длина номера 3-20 символов")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Введите адрес")]
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Длина адреса 3-100 символов")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Введите описание")]
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Длина описания 3-100 символов")]
        public string Description { get; set; } = string.Empty;

        public string FullName
        {
            get { return SurName + " " + Name + " " + LastName; }
        }
    }
}
