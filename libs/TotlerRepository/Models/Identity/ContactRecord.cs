using TotlerRepository.Models.Entity;

namespace TotlerRepository.Models.Identity
    {
    public class ContactRecord : BaseEntity
        {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ContactPhone { get; set; }
        public string SkypeId { get; set; }
        public string IcqId { get; set; }
        public ApplicationUser User { get; set; }
        }
    }
