using System.Collections.Generic;

namespace Trade_MVC5.Models.Identity
    {
    public static class Roles
        {
        private static readonly string[] RolesFixed = { "Admin" };

        public static string Admin => RolesFixed[0];

        public static IEnumerable<string> All => RolesFixed;
        }
    }
