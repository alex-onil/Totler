using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trade_MVC6.ViewModels.Home
{
    public class UserStatusViewModel
    {
     public bool IsAdmin { get; set; }
     public bool IsAuthenticated { get; set; }
    }
}
