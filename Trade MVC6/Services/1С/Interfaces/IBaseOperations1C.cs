using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trade_MVC6.Services._1С.Interfaces
{
    public interface IBaseOperations1C<TEntity>
    {
        Task<ICollection<TEntity>> QueryAsync();
        Task<TEntity> GetAsync(string id);
    }
}
