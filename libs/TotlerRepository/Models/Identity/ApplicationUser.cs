using Microsoft.AspNet.Identity.EntityFramework;

namespace TotlerRepository.Models.Identity
    {
    public class ApplicationUser : IdentityUser
        {
        public string CompanyName { get; set; }

        public ContactRecord Contact { get; set; }

        public string Account1CId { get; set; }

        public static ApplicationUser Admin =>
           new ApplicationUser
               {
               UserName = "root",
               Email = "root@totler.by"
               };

        }
    }
