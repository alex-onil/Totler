using System.Collections.Generic;
using System.Threading.Tasks;
using DataTypes.Models;

namespace ApiConnection.Interfaces
{
    public interface IApiModelGroups<in TModel, TCollection, TResultProductTypeCollection> 
            where TCollection : ICollection<TModel>, new()

{
    /// <summary>
    /// Получает корневой элемент моделей
    /// </summary>
    /// <returns>Модель, являющуюся корневой</returns>
    Task<TCollection> GetModelGroupsRootAsync();

    /// <summary>
    /// Получает список товаров находящихся в группе моделей
    /// </summary>
    /// <param name="modelGroup">Группа моделей</param>
    /// <param name="startPosition">Стартовая позиция</param>
    /// <param name="count">Количество элементов</param>
    /// <returns></returns>
    Task<TResultProductTypeCollection> ReadProductsByModelAsync(TModel modelGroup, int startPosition = 0, int count = 0);

    /// <summary>
    /// Возвращает наследников указанного элемента дерева группы моделей
    /// </summary>
    /// <param name="modelGroup">Группа моделей</param>
    /// <param name="startPosition">Стартовая позиция в списке</param>
    /// <param name="count">Количество элементов</param>
    /// <returns></returns>
    Task<TCollection> ReadModelChildsByModelGroupAsync(TModel modelGroup, int startPosition = 0, int count = 0);
}
}