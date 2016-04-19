using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using System.Collections.ObjectModel;
using ApiConnection.Infrastructure;
using DataTypes.Models;

namespace ApiConnection.Extensions
    {
    //public static class CrudExtensions
    //    {

    //    #region Product by Model Extensions

    //    public static async Task<bool> CreateProductAsync(this ModelGroup modelGroup, ApiClient client, ShippedProduct product)
    //      {
    //       if (client.IsConnected())
    //        {
    //           product.Model = modelGroup;
    //             try
    //              {
    //               await client.Products.CreateAsync(product);
    //               return true;
    //              }
    //              catch (Exception) { return false; }
    //         } else { return false; }
    //      }  

    //    #endregion

    //    #region ModelGroupsExtensions

    //    /// <summary>
    //    /// Возвращает список продуктов в модельной группе
    //    /// </summary>
    //    /// <param name="Model_Group">Модельная группа</param>
    //    /// <param name="ModelGroupsApi">API доступа к серверу</param>
    //    /// <param name="Start_Position">Стартовая позиция</param>
    //    /// <param name="Count">Количество элементов</param>
    //    /// <returns></returns>
    //    public static async Task<ObservableCollection<ShippedProduct>> GetProducts(this ModelGroup Model_Group,
    //                                                    ApiModelGroups ModelGroupsApi,
    //                                                    int Start_Position = 0,
    //                                                    int Count = 0)
    //        {
    //         return await ModelGroupsApi.ReadProductsByModelAsync(Model_Group, Start_Position, Count);
    //        }

    //         /// <summary>
    //         /// Возвращает список наследников в модельной группе
    //         /// </summary>
    //         /// <param name="Model_Group">Модельная группа</param>
    //         /// <param name="ModelGroupsApi">API доступа к серверу</param>
    //         /// <param name="Start_Position">Стартовая позиция</param>
    //         /// <param name="Count">Количество элементов</param>
    //         public static async Task<IEnumerable<ModelGroup>> GetChildModelGroups(this ModelGroup Model_Group,
    //                                                         ApiModelGroups ModelGroupsApi,
    //                                                         int Start_Position = 0,
    //                                                         int Count = 0)
    //             {
    //             return await ModelGroupsApi.ReadModelChildsByModelGroupAsync(Model_Group, Start_Position, Count);
    //             }

    //    #endregion
    //    }
    }
