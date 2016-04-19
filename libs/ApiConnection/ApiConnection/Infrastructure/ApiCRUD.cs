using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ApiConnection.Interfaces;
using Newtonsoft.Json;

namespace ApiConnection.Infrastructure
    {
        /// <summary>
        /// Класс реализующий CRUD для элемента API
        /// </summary>
        /// <typeparam name="TModel">Тип для запроса от API сервера</typeparam>
        /// <typeparam name="TCollection"> Тип получаемой коллекции</typeparam>
        public class ApiCrud<TModel, TCollection> : IApiCrud<TModel, TCollection>
                     where TCollection : ICollection<TModel>, new()
        {

         #region Инициализация

             protected readonly ApiClient Client;
             protected readonly string Uri;

             /// <summary>
             /// Конструктору необходимо предоставить данные
             /// об Клиенте и относительном адресе API для операций CRUD
             /// </summary>
             /// <param name="client">Клиент на котором делать операции</param>
             /// <param name="uri">относительный адрес API</param>
             public ApiCrud(ApiClient client, string uri)
              {
                Uri = uri;
                Client = client;
              }

         #endregion

        #region Методы доступа

        /// <summary>
        /// Создает запись на сервере указанного типа
        /// </summary>
        /// <param name="item">Элемент для создания на сервере</param>
        public virtual async Task<TModel> CreateAsync(TModel item)
        {
               var result = await CreateAsync(new [] {item});
               return result.FirstOrDefault();
        }

        /// <summary>
        /// Создает запись на сервере указанного типа
        /// </summary>
        /// <param name="items">Элементы для создания на сервере</param>
        public virtual async Task<TCollection> CreateAsync(IList<TModel> items)
            {

            var response = Client.SendRequestAsync(Uri, HttpMethod.Post, items);

            return await Task.Run(async () =>
                       JsonConvert.DeserializeObject<TCollection>(await response));
            }

        /// <summary>
        /// Чтение элементов на сервере указанного типа
        /// </summary>
        /// <param name="startPosition">Стартовая позиция</param>
        /// <param name="count">Количество элементов для чтения</param>
        /// <returns>Возвращает прочитанные элементы или вызывает ошибку</returns>
        public virtual async Task<TCollection> ReadAsync(int startPosition = 0, int count = 0)
            {

            var url = string.Format(Uri + "/{0}/{1}", startPosition, count);

            var response = Client.SendRequestAsync(url, HttpMethod.Get);

            return await Task.Run(async () =>
                         JsonConvert.DeserializeObject<TCollection>(await response));
            }

        /// <summary>
        /// Получает количество обьектов указанного типа на сервере
        /// </summary>
        /// <returns>Количество обьектов</returns>
        public virtual async Task<int> CountAsync()
            {
                var response = Client.SendRequestAsync(Uri, HttpMethod.Head);

                return await Task.Run(async () =>
                    JsonConvert.DeserializeObject<int>(await response));

            }


        /// <summary>
        /// Обновляет записи на сервере указанного типа
        /// </summary>
        /// <param name="items">Запись для обновления</param>
        public virtual async Task<TCollection> UpdateAsync(IList<TModel> items)
            {

            var response = Client.SendRequestAsync(Uri, HttpMethod.Put, items);

            return await Task.Run(async () =>
                        JsonConvert.DeserializeObject<TCollection>(await response));

            }

        /// <summary>
        /// Обновляет записи на сервере указанного типа
        /// </summary>
        /// <param name="item">Запись для обновления</param>
        public virtual async Task<TModel> UpdateAsync(TModel item)
            {
            var message = await UpdateAsync(new [] { item });
            return message == null ? default(TModel) : message.First();
            }

        /// <summary>
        /// Удаляет записи на сервере указанного типа
        /// </summary>
        /// <param name="items">Элемент для удаления</param>
        public virtual async Task DeleteAsync(IList<TModel> items)
            {
            await Client.SendRequestAsync(Uri, HttpMethod.Delete, items);

            }

        /// <summary>
        /// Удаляет запись на сервере указанного типа
        /// </summary>
        /// <param name="item">Элемент для удаления</param>
        public async Task DeleteAsync(TModel item)
            {
            await DeleteAsync(new [] { item });
            }

        #endregion
        }
    }
