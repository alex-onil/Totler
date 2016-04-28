using System.ComponentModel.DataAnnotations;

namespace Trade_MVC6.Models.Account
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль {0} должен быть не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        }
}
