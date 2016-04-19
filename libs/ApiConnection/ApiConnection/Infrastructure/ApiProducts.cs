using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiConnection.Infrastructure;
using DataTypes;

namespace ApiConnection
    {
    /// <summary>
    /// Класс расширения CRUD для продуктов
    /// </summary>
    public class ApiProducts : ApiCRUD<ShippedProduct>
        {
         
         #region Инициализация

          public ApiProducts(ApiClient client, string Uri) : base(client, Uri) {}

         #endregion

        }
    }
