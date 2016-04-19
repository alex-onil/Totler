using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;

namespace ApiConnection
    {
    /// <summary>
    /// Класс опций подключения к серверу
    /// </summary>
    public static class ApiConnectionOptions
        {
           /// <summary>
           /// Возвращает готовый обьект параметров подключения
           /// </summary>
           /// <param name="Addres">Адрес сервера</param>
           /// <param name="Login">Логин пользователя</param>
           /// <param name="Password">Пароль пользователя</param>
           /// <returns></returns>
           public static AuthModel Connection(string Addres, string Login, String Password)
              {
               return new AuthModel() { Addr = Addres,
                                        Login = Login,
                                        PasswordValue = Password };
              }
        }
    }
