using System.ComponentModel.DataAnnotations;
using Trade_MVC6.ViewModels.Shared;

namespace Trade_MVC6.ViewModels.Account
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "Длинна должна быть в пределах от {2} до {1} символов.", MinimumLength = 3 )]
        [Display(Name = "Псевдоним")]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Название компании")]
        public string CompanyName { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool Access1C { get; set; }

        public ContactViewModel Contact { get; set; }
        }
}
