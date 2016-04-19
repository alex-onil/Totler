using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Threading;
using System.Web;
// using Newtonsoft.Json;
 
namespace ConsoleClient
{
    class Program
    {
        private const string APP_PATH= "http://localhost:63039";
        private static string token;
 
        static void Main(string[] args)
        {
           using (HttpClient client = new HttpClient())
            {
                   Console.WriteLine("Доступ без авторизации:");
                   client.BaseAddress = new Uri(APP_PATH);

                   HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/AuthorizedUserName");
                   HttpResponseMessage response = client.SendAsync(request).Result;
                   // response.EnsureSuccessStatusCode();
                   Console.WriteLine("Response code:" + response.StatusCode);
                   if (response.IsSuccessStatusCode)
                   Console.WriteLine("Response from api:" + response.Content.ReadAsStringAsync().Result);
                       else { Console.WriteLine("Response Error"); }


                   //var query = HttpUtility.ParseQueryString(string.Empty);
                   //query["Option"] = "Fuck";

                   //request = new HttpRequestMessage(HttpMethod.Get, "/NameA?" + query.ToString());

                   //response = client.SendAsync(request).Result;
                   //// response.EnsureSuccessStatusCode();
                   //Console.WriteLine("Response code:" + response.RequestMessage);
                   ////if (response.IsSuccessStatusCode)
                   ////    Console.WriteLine("Response from /NameA:" + response.Content.ReadAsStringAsync().Result);
                   ////else { Console.WriteLine("Response Error"); }


                    Console.WriteLine("Введите логин:");
                    string userName = Console.ReadLine();
 
                    Console.WriteLine("Введите пароль:");
                    string password = Console.ReadLine();
 
                    //var registerResult = Register(userName, password);
 
                    //Console.WriteLine("Статусный код регистрации: {0}", registerResult);
 
                    Dictionary<string, string> tokenDictionary = GetTokenDictionary(userName, password);

                    token = tokenDictionary.ContainsKey("access_token")? tokenDictionary["access_token"] : "Key not found";
 
                    Console.WriteLine();
                    Console.WriteLine("Access Token:");
                    Console.WriteLine(token);

                   client.DefaultRequestHeaders.Authorization =
                       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); 

                    Console.WriteLine("Доступ после авторизации:");



                    request = new HttpRequestMessage(HttpMethod.Get, "api/AuthorizedUserName");
                    response = client.SendAsync(request).Result;
                    //response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("Response from api:" + response.Content.ReadAsStringAsync().Result);
                    else { Console.WriteLine("Response Error"); }


                    request = new HttpRequestMessage(HttpMethod.Get, "api/ModelGroups");
                    response = client.SendAsync(request).Result;
                    //response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("Response from api ModelGroups:" + response.Content.ReadAsStringAsync().Result);
                    else { Console.WriteLine("Response Error"); }

                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["Option"] = "Autorized Fuck";

                    request = new HttpRequestMessage(HttpMethod.Get, "/NameA?" + query.ToString());

                    response = client.SendAsync(request).Result;
                    // response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("Response from /NameA:" + response.Content.ReadAsStringAsync().Result);
                    else { Console.WriteLine("Response Error"); }

            }



            //Console.WriteLine();
            //string userInfo = GetUserInfo(token);
            //Console.WriteLine("Пользователь:");
            //Console.WriteLine(userInfo);
 
            //Console.WriteLine();
            //string values = GetValues(token);
            //Console.WriteLine("Values:");
            //Console.WriteLine(values);
 
            Console.Read();
        }
 
        // регистрация
//        static string Register(string email, string password)
//        {
//            var registerModel = new
//            {
//                Email = email,
//                Password = password,
//                ConfirmPassword = password
//            };
//            using (var client = new HttpClient())
//            {
//                var serializer = new JavaScriptSerializer();
//                var json_str = serializer.Serialize(registerModel);
                
//                //var request = new HttpRequestMessage(HttpMethod.Post, APP_PATH + "/api/Account/Register");

//                //request.Content = new StringContent(json_str);

//                var response = client.PostAsync(APP_PATH + "/token", new StringContent(json_str)).Result;


////                 var response = client.PostAsJsonAsync(APP_PATH + "/api/Account/Register", registerModel).Result;
//                return response.StatusCode.ToString();
//            }
//        }
//        // получение токена
        static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ), 
                    new KeyValuePair<string, string>( "username", userName ), 
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);
 
            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(APP_PATH + "/token", content).Result;
                
                if (response.IsSuccessStatusCode) Console.WriteLine("Результат удачен");
                         else Console.WriteLine("Результат ошибочен");

                Console.WriteLine("Код отклика :" + response.StatusCode);
                Console.WriteLine("Ответ :" + response.Content.Headers);

                var result = response.Content.ReadAsStringAsync().Result;

                // Десериализация полученного JSON-объекта
                var serializer = new JavaScriptSerializer();

                Dictionary<string, string> tokenDictionary = serializer.Deserialize<Dictionary<string, string>>(result);

                return tokenDictionary;
            }
        }
        // создаем http-клиента с токеном 
        static HttpClient CreateClient(string accessToken = "")
            {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
                {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                }
            return client;
            }

        // получаем информацию о клиенте 
        //static string GetUserInfo(string token)
        //    {
        //    using (var client = CreateClient(token))
        //        {
        //        var response = client.GetAsync(APP_PATH + "/api/Account/UserInfo").Result;
        //        return response.Content.ReadAsStringAsync().Result;
        //        }
        //    }

        //// обращаемся по маршруту api/values 
        //static string GetValues(string token)
        //    {
        //    using (var client = CreateClient(token))
        //        {
        //        var response = client.GetAsync(APP_PATH + "/api/values").Result;
        //        return response.Content.ReadAsStringAsync().Result;
        //        }
        //    }
    }
    }


