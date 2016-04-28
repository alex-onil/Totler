using System.Collections.Generic;
using System.Threading.Tasks;

namespace Totler1C.BLL.Interfaces
{
    public interface IBaseOperations1C<TEntity>
    {
        Task<ICollection<TEntity>> QueryAsync();
        Task<TEntity> GetAsync(string id);
    }
}
