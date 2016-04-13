using System.Collections.Generic;

namespace Trade_MVC6.Models.Identity
    {
    public static class Roles
        {
        private static readonly string[] RolesFixed = { "Admin", "User1C" };

        public static string Admin => RolesFixed[0];

        public static string User1C => RolesFixed[1];

        public static IEnumerable<string> All => RolesFixed;
        }
    }
