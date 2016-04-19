using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiConnection.Interfaces;
using DataTypes.Models;
using Newtonsoft.Json;

namespace ApiConnection.Infrastructure
    {


        /// <summary>
    /// Класс расширения CRUD для моделей групп
    /// </summary>
    public class ApiModelGroups<TCollection> : ApiCrud<ModelGroup, TCollection>, IApiModelGroups<ModelGroup, TCollection, IList<ShippedProduct>> 
                    where TCollection : ICollection<ModelGroup>, new()
        {

         #region Инициализация

        public ApiModelGroups(ApiClient client, string uri) : base(client, uri) { }

        #endregion

        /// <summary>
        /// Получает корневой элемент моделей
        /// </summary>
        /// <returns>Модель, являющуюся корневой</returns>
        public virtual async Task<TCollection> GetModelGroupsRootAsync()
            {
            var response = await Client.SendRequestAsync(Uri + "/Root", HttpMethod.Get);

            return await Task.Run(() => JsonConvert.DeserializeObject<TCollection>(response));
            }

        /// <summary>
        /// Получает список товаров находящихся в группе моделей
        /// </summary>
        /// <param name="modelGroup">Группа моделей</param>
        /// <param name="startPosition">Стартовая позиция</param>
        /// <param name="count">Количество элементов</param>
        /// <returns></returns>
        public virtual async Task<IList<ShippedProduct>> ReadProductsByModelAsync(ModelGroup modelGroup, int startPosition = 0, int count = 0)
            {

            var url = string.Format(Uri + "/Products/{0}/{1}", startPosition, count);

            var response = Client.SendRequestAsync(url, HttpMethod.Post, modelGroup);

            return await Task.Run(async () => JsonConvert.DeserializeObject<IList<ShippedProduct>>(await response));
            }


        /// <summary>
        /// Возвращает наследников указанного элемента дерева группы моделей
        /// </summary>
        /// <param name="modelGroup">Группа моделей</param>
        /// <param name="startPosition">Стартовая позиция в списке</param>
        /// <param name="count">Количество элементов</param>
        /// <returns></returns>
        public virtual async Task<TCollection> ReadModelChildsByModelGroupAsync(ModelGroup modelGroup, int startPosition = 0, int count = 0)
            {

            var url = string.Format(Uri + "/Childs/{0}/{1}", startPosition, count);

            var response = Client.SendRequestAsync(url, HttpMethod.Post, modelGroup);

            return await Task.Run(async () => JsonConvert.DeserializeObject<TCollection>(await response));

            }

        }
    }
