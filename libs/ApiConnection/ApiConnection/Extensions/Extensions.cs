using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Diagnostics.Contracts;
using System.Security.Authentication;
using System.Web.Script.Serialization;
using DataTypes;

namespace ApiConnection
    {
    public static class ProductsExtensions
        {

        public static readonly string root = "api";

        public static HttpClient CreateClient(this ApiClient client)
            {
            var _client = new HttpClient();
            if (client.IsConnected())
                {
                  _client.DefaultRequestHeaders.Authorization =
                     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", client._token);
                  _client.BaseAddress = new Uri(client.Uri);
                }
            return _client;
            }

        public static bool IsConnected(this ApiClient client)
            {
              if (client == null) return false; else return (client._token != null);
            }

        public static async Task<string> GetUserNameAsync(this ApiClient client)
            {
            // Contract.Requires<Exception>(client.IsAuthentificated(), "Client not Authenticated");
            using (var _client = client.CreateClient())
                {
                 return new JavaScriptSerializer().Deserialize<string>
                                (await _client.GetStringAsync(root + "/AuthorizedUserName"));
                }
            }
         public static async Task<string> Test(this ApiClient client)
          {
          using (var _client = client.CreateClient())
              {
               // var reqest = new HttpRequestMessage(HttpMethod.Post, root + "/ModelGroups");

               var model = new ModelGroup() { ModelGroupName = "Закваска" };

               var buf = new JavaScriptSerializer().Serialize(model);

               var model2 = new JavaScriptSerializer().Deserialize<ModelGroup>(buf);
               // reqest.Content = new StringContent(buf, Encoding.UTF8);

               var response = await _client.PostAsync(root + "/ModelGroups", new StringContent(buf));

               // var response = await _client.SendAsync(reqest);

               if (!response.IsSuccessStatusCode) throw new Exception("Ошибка запроса:" + response.StatusCode);

               return await response.Content.ReadAsStringAsync();
               //return new JavaScriptSerializer().Deserialize<string>
               //              (await _client.GetStringAsync(root + "/AuthorizedUserName"));
              }
          }
        }
    }
