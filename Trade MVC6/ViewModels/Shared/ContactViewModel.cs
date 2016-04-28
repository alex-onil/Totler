using System.ComponentModel.DataAnnotations;

namespace Trade_MVC6.ViewModels.Shared
{
    public class ContactViewModel
    {
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
