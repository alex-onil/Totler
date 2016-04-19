using System.ComponentModel.DataAnnotations;

namespace Trade_MVC6.ViewModels.Account
{
    public class RegisterViewModel : AbstractValidator
        {
        [Required]
        [StringLength(15, ErrorMessage = "Длинна должна быть в пределах от {0} до {2} символов.", MinimumLength = 3 )]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Длинна должна быть в пределах от {0} до {2} символов.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; }

        public string SkypeId { get; set; }

        public string IcqId { get; set; }

        }
}
