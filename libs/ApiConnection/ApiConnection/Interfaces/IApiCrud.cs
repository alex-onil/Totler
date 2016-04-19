using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiConnection.Interfaces
    {
    public interface IApiCrud<TModel, TCollection> where TCollection : ICollection<TModel>, new()
        {
        /// <summary>
        /// Создает запись на сервере указанного типа
        /// </summary>
        /// <param name="item">Элемент для создания на сервере</param>
        Task<TModel> CreateAsync(TModel item);

        /// <summary>
        /// Создает запись на сервере указанного типа
        /// </summary>
        /// <param name="items">Элементы для создания на сервере</param>
        Task<TCollection> CreateAsync(IList<TModel> items);

        /// <summary>
        /// Чтение элементов на сервере указанного типа
        /// </summary>
        /// <param name="startPosition">Стартовая позиция</param>
        /// <param name="count">Количество элементов для чтения</param>
        /// <returns>Возвращает прочитанные элементы или вызывает ошибку</returns>
        Task<TCollection> ReadAsync(int startPosition = 0, int count = 0);

        /// <summary>
        /// Получает количество обьектов указанного типа на сервере
        /// </summary>
        /// <returns>Количество обьектов</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Обновляет записи на сервере указанного типа
        /// </summary>
        /// <param name="items">Запись для обновления</param>
        Task<TCollection> UpdateAsync(IList<TModel> items);

        /// <summary>
        /// Обновляет записи на сервере указанного типа
        /// </summary>
        /// <param name="item">Запись для обновления</param>
        Task<TModel> UpdateAsync(TModel item);

        /// <summary>
        /// Удаляет записи на сервере указанного типа
        /// </summary>
        /// <param name="items">Элемент для удаления</param>
        Task DeleteAsync(IList<TModel> items);

        /// <summary>
        /// Удаляет запись на сервере указанного типа
        /// </summary>
        /// <param name="item">Элемент для удаления</param>
        Task DeleteAsync(TModel item);
        }
    }
