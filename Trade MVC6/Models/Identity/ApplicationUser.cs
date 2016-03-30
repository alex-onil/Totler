using Microsoft.AspNet.Identity.EntityFramework;

namespace Trade_MVC6.Models.Identity
    {
    public class ApplicationUser : IdentityUser
        {
        public static ApplicationUser Admin => 
                    new ApplicationUser
                    {
                        UserName = "root", 
                           
                    };
        }
    }
