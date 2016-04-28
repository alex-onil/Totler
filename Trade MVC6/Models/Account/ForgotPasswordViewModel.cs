using System.ComponentModel.DataAnnotations;

namespace Trade_MVC6.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        }
}
