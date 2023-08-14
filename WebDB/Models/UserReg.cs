using System.ComponentModel.DataAnnotations;

namespace WebDB.Models
{
    public class UserReg
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Ошибка! Необходимо ввести логин.")]
        [StringLength(50, ErrorMessage = "Ошибка! Логин не может быть длиннее 50 символов.")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Ошибка! Необходимо ввести пароль.")]
        [StringLength(50, ErrorMessage = "Ошибка! Пароль не может быть длиннее 50 символов.")]
        public string Password { get; set; }

        [Display(Name = "Повторите пароль")]
        [Required(ErrorMessage = "Ошибка! Необходимо ввести пароль повторно.")]
        [StringLength(50, ErrorMessage = "Ошибка! Пароль не может быть длиннее 50 символов.")]
        [Compare("Password", ErrorMessage = "Ошибка! Пароли должны совпадать.")]
        public string PasswordConfirm { get; set; }
    }
}
