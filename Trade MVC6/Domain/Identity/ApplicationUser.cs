using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Trade_MVC5.Domain.Identity
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
