using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trade_MVC6.Services._1С.Interfaces;

namespace Trade_MVC6.Services._1С
{
    public class Provider1C : IProvider1C
    {
        public Provider1C(IUsers1C users, IGoodsGroups1C groups, IGoods1C products)
        {
            Users = users;
            Groups = groups;
            Products = products;
        }

        public IUsers1C Users { get; }
        public IGoodsGroups1C Groups { get; }
        public IGoods1C Products { get; }
    }
}
