using Microsoft.AspNet.Identity.EntityFramework;
using Trade_MVC6.Models.Identity.AccountDetails;

namespace Trade_MVC6.Models.Identity
    {
    public class ApplicationUser : IdentityUser
        {
        public string CompanyName { get; set; }
        public int? ContactRef { get; set; }
        public ContactRecord Contact { get; set; }
        public string Account1CId { get; set; }
        public bool Access1C { get; set; }

        public static ApplicationUser Admin =>
           new ApplicationUser
               {
               UserName = "root@totler.by",

               };

        }
    }
