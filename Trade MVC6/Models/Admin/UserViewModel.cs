using System.ComponentModel.DataAnnotations;
using TotlerRepository.ModelValidation;
using Trade_MVC6.Models.Shared;

namespace Trade_MVC6.Models.Admin
{
    public class UserViewModel : AbstractValidator
        {
        public string Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Длинна должна быть в пределах от {2} до {1} символов.", MinimumLength = 3)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool Access1C { get; set; }

        public ContactViewModel Contact { get; set; }

        public string Account1CId { get; set; }
        }
}
