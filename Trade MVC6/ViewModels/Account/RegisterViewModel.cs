using System.ComponentModel.DataAnnotations;

namespace Trade_MVC6.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "Длинна должна быть в пределах от {0} до {2} символов.", MinimumLength = 3 )]
        [Display(Name = "Псевдоним")]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Длинна должна быть в пределах от {0} до {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "Название компании")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string ContactPhone { get; set; }

        [Display(Name = "Skype")]
        public string SkypeId { get; set; }

        [Display(Name = "ICQ")]
        public string IcqId { get; set; }

        }
}
