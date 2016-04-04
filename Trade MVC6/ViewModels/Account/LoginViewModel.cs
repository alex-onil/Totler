using System.ComponentModel.DataAnnotations;

namespace Trade_MVC6.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить ?")]
        public bool RememberMe { get; set; }
    }
}
