using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataTypes
    {
    /// <summary>
    /// Модель авторизации для клиента
    /// </summary>
    [DataContract]
    public class AuthModel
        {
         /// <summary>
         /// Адрес сервера
         /// </summary>
         [DataMember]
         public string Addr {get; set;}

         /// <summary>
         /// Пароль пользователя
         /// </summary>
         [DataMember]
         public string PasswordValue {get; set;}

         /// <summary>
         /// Логин пользователя
         /// </summary>
         [DataMember]
         public string Login {get; set;}
        }
    }
