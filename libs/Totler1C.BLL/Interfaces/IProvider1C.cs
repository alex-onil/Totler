namespace Totler1C.BLL.Interfaces
{
    public interface IProvider1C
    {
      IUsers1C Users { get; }
      IGoodsGroups1C Groups { get; }
      IGoods1C Products { get; }
    }
}
