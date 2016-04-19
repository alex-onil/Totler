using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using System.Linq.Expressions;
using System.Reflection;
using DataTypes.Models;
using DataTypes.ViewModels;

namespace DataTypes.Extensions
    {
    public static class Extensions
        {
        /// <summary>
        /// Применяет к записи автора, с занесением текущей даты
        /// </summary>
        /// <param name="author">Автор записи</param>
        /// <returns>Замыкает на себя</returns>
        public static StoreId ApplyAuthor(this StoreId item, string author)
            {
            item.AuthorId = author;
            item.CreationDate = DateTime.Now;
            return item;
            }

        /// <summary>
        /// Применяет автора модификаций к записи
        /// </summary>
        /// <param name="editor">Автор изменений</param>
        /// <returns>Замыкает на себя</returns>
        public static StoreId ApplyEditor(this StoreId item, string editor)
            {
            item.LastChangedUserId = editor;
            item.LastModificationDate = DateTime.Now;
            return item;
            }

        /// <summary>
        /// Заменяет значения автора для записи и всех наследников
        /// </summary>
        /// <param name="modelGroup">Группа моделей продукции для применения</param>
        /// <param name="author">Автор для указания</param>
        public static IEnumerable<ModelGroup> SetChildAuthor(this IEnumerable<ModelGroup> modelGroup, string author)
            {

            foreach (var group in modelGroup)
                {
                group.AuthorId = author;
                if (group.Childs.Count > 0) group.Childs.SetChildAuthor(author);
                }

            return modelGroup;
            }

        /// <summary>
        /// Заменяет значения последнего изменившего для записи и всех наследников
        /// </summary>
        /// <param name="modelGroup">Группа моделей продукции для применения</param>
        /// <param name="author">Автор для указания</param>
        public static IEnumerable<ModelGroup> SetChildUpdateAuthor(this IEnumerable<ModelGroup> modelGroup, string author)
            {

            foreach (var group in modelGroup)
                {
                group.LastChangedUserId = author;
                if (group.Childs.Count > 0) group.Childs.SetChildUpdateAuthor(author);
                }

            return modelGroup;
            }


        /// <summary>
        /// Осуществление поиска Модели среди наследников группы моделей
        /// </summary>
        /// <param name="modelGroup">Группа моделей продукции для поиска</param>
        /// <param name="predicat">Функция предикат поиска элемента</param>
        /// <returns>Возвращает найденную группу моделей</returns>
        public static ModelGroupViewModel FindInChildren(this IEnumerable<ModelGroupViewModel> modelGroup, Func<ModelGroupViewModel, bool> predicat)
            {
            foreach (var child in modelGroup)
                {
                if (predicat(child)) return child;
                if (child.Childs.Count > 0)
                    {
                    var result = child.Childs.FindInChildren(predicat);
                    if (result != null) return result;
                    }
                }
            return null;
            }


        ///// <summary>
        ///// Выполняет указанную операци. над всеми наследниками группы моделей
        ///// </summary>
        ///// <param name="Model_Group">Группа моделей продукции для применения</param>
        ///// <param name="MGFunc">Функция для выполнения над каждым наследником моделей</param>
        ///// <returns>Возвращает ссылку на себя</returns>
        //public static IEnumerable<ModelGroup> ActionOnChilds(this IEnumerable<ModelGroup> Model_Group, Action<ModelGroup> MGFunc)
        //  {
        //   foreach(var _child in Model_Group)
        //      {
        //       if (_child.Childs.Count > 0 ) _child.Childs.ActionOnChilds(MGFunc);
        //       MGFunc(_child);
        //      }
        //   return Model_Group;
        //  }

        ///// <summary>
        ///// Выполняет указанную операци. над всеми продуктами
        ///// </summary>
        ///// <param name="_products">Продукты для применения</param>
        ///// <param name="ProductFunction">Функция для выполнения над каждым продуктом</param>
        ///// <returns>Возвращает ссылку на себя</returns>
        //public static IEnumerable<T> ActionOn<T>(this IEnumerable<T> _products, Action<T> ProductFunction)
        //                            where T:ShippedProduct
        //    {
        //    foreach (var _child in _products)
        //        {
        //         ProductFunction(_child);
        //        }
        //    return _products;
        //    }



        //public static T CopyPropertiesTo<T>(this IEnumerable<PropertyInfo> properties, 
        //        T Source, T Destination) where T : class
        //        {
        //          foreach(var property in properties)
        //            {
        //             if (typeof(T) == property.DeclaringType && property.CanRead && property.CanWrite)
        //               {
        //                property.SetValue(Destination, property.GetValue(Source));
        //               }
        //            }
        //            return Destination;
        //        } 

        ///// <summary>
        ///// Копирует все открытые свойства в получаетеля, возвращает получателя
        ///// </summary>
        ///// <param name="_source">Источник для копирования свойств</param>
        ///// <param name="Target">Получатель копируемых свойств</param>
        ///// <returns>Возвращает получателя</returns>
        //public static T CopyPropertiesTo<T>(this T _source, T Target) where T : class
        //  {
        //     var Properties = typeof(T).GetProperties();

        //     foreach(var _property in Properties)
        //       {
        //        if (_property.CanRead)
        //         {
        //            var have_value = _property.GetValue(_source);

        //            if (_property.CanWrite && !_property.GetGetMethod().IsVirtual)
        //               _property.SetValue(Target, have_value);
        //          }
        //       }
        //     return Target;
        //  }

        ///// <summary>
        ///// Возвращает копию элемента без наследников
        ///// </summary>
        ///// <typeparam name="T">Тип элемента</typeparam>
        ///// <param name="Model_Group">Источник</param>
        // public static T Simple<T>(this T Model_Group) where T: class, new()
        //    {
        //     return Model_Group.CopyPropertiesTo(new T());
        //    }

        ///// <summary>
        ///// Копирует все свойства в получателя, кроме массивов
        ///// </summary>
        ///// <typeparam name="T">Тип элемента</typeparam>
        ///// <param name="Model_Group">Получатель</param>
        ///// <param name="Model_Group_Source">источник</param>
        //public static T UpdateProperties<T>(this T Model_Group, T Model_Group_Source)
        //   {
        //     var Properties = typeof(T).GetProperties();
        //     foreach(var _property in Properties)
        //       {
        //        var Types = typeof(StoreId).GetProperties();

        //        // Проверим на свойства из группы безопастности
        //        if (!Types.Any(t => t.Name.Equals(_property.Name)))

        //            // Обновим свойства если они только не коллекция вложенных элементов
        //            if (!_property.PropertyType.Equals(typeof(ICollection<T>)))
        //                // Вложенные типы обновлять не будем
        //               {
        //                var have_value = _property.GetValue(Model_Group);
        //                var set_value = _property.GetValue(Model_Group_Source);

        //                _property.SetValue(Model_Group, _property.GetValue(Model_Group_Source));


        //               }
        //       } 
        //     return Model_Group;
        //   }
        }
    }
