using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Trade_MVC6.Models.Identity.AccountDetails;

namespace Trade_MVC6.Models.Identity
    {
    public class ApplicationUser : IdentityUser
        {
        public string CompanyName { get; set; }

        public int ContactForeignKey { get; set; }

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
