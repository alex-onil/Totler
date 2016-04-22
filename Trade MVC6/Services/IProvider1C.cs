using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trade_MVC6.Services._1С.Interfaces;

namespace Trade_MVC6.Services
{
    public interface IProvider1C
    {
      IUsers1C Users { get; }
      IGoodsGroups1C Groups { get; }
      IGoods1C Products { get; }
    }
}
