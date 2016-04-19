using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTypes;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Converters;

namespace ApiConnection
{
    public class ApiClient
    {
        #region Конфигурирование настроек

        private const int RetryCount = 3;

        public const string Root = "api";

        #endregion


        public class ConnectionArgs : EventArgs
        {
         public bool ConnectionStatus { get; set; }
        }

      public event EventHandler<ConnectionArgs> ConnectionStatusChanged;

      private void _ConnectionStatucChanged(ConnectionArgs args)
      {
          ConnectionStatusChanged?.Invoke(this, args);
      }

       public bool IsConnected => Token != null;

        public HttpClient CreateClient
            {
            get
                {
                if (!IsConnected) throw new Exception("Not Connected");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                client.BaseAddress = new Uri(Uri);
                return client;
                }
            }



        #region Инициализации и получение клиента

        private AuthModel _apiServer;

      public string Uri => _apiServer.Addr;

      internal string Token;

      /// <summary>
      /// Подключится к серверу
      /// </summary>
      /// <param name="option">Параметры подключения</param>
      /// <returns>Статус подключения</returns>
      public async Task<bool> ConnectAsync(AuthModel option)
        {
           if (!String.IsNullOrEmpty(Token)) return true;

          Dictionary<string, string> tokenDictionary;
          try
          {
              tokenDictionary = await GetTokenDictionaryAsync(option);
          }
          catch (Exception)
          {
                Token = null;
                return false;
            }

          if (tokenDictionary == null) return false;

           Token = tokenDictionary.ContainsKey("access_token") ? tokenDictionary["access_token"] : null;
           
           _apiServer = option;

           _ConnectionStatucChanged(new ConnectionArgs { ConnectionStatus = IsConnected }); 

           return (Token != null);       
        }

        /// <summary>
        /// Отключится от сервера
        /// </summary>
      public void Disconnect()
       {
            if (!IsConnected) return;
            Token = null;
            _apiServer = null;
            _ConnectionStatucChanged(new ConnectionArgs { ConnectionStatus = IsConnected });
       }

        private async Task<Dictionary<string, string>> GetTokenDictionaryAsync(AuthModel server)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", server.Login ),
                    new KeyValuePair<string, string>( "Password", server.PasswordValue )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response = await
                            client.PostAsync(server.Addr + "/token", content);

                if (!response.IsSuccessStatusCode) return null;

                var result = await response.Content.ReadAsStringAsync();

                return await Task.Run(() =>
                      JsonConvert.DeserializeObject<Dictionary<string, string>>(result));
        }
    }

        public string GetUserName
            {
            get
                {

                var result = Task.Run(async () =>
                {
                    using (var client = CreateClient)
                        {
                        var message = await client.GetStringAsync(Root + "/AuthorizedUserName");
                        return JsonConvert.DeserializeObject<string>(message);
                        }
                });
                Task.WaitAll(result);
                return result.Result;
                }
            }


        #endregion

        #region Sending / Receving Data

        public async Task<string> SendRequestAsync(string uri, HttpMethod method, object items)
        {
            var serializedItems = Task.Run(() => JsonConvert.SerializeObject(items, new IsoDateTimeConverter()));

            using (var client = CreateClient)
            {
                var retryCount = 0;
                HttpResponseMessage response;
                do
                {

                    var request = new HttpRequestMessage(method, uri)
                    {
                        Content = new StringContent(await serializedItems)
                    };
                    response = await client.SendAsync(request);

                }
                while (!response.IsSuccessStatusCode && retryCount++ < RetryCount);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Ошибка получения данных");

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> SendRequestAsync(string uri, HttpMethod method)
        {

                var retryCount = 0;
                HttpResponseMessage response;
                do
                {
                var request = new HttpRequestMessage(method, uri);
                using (var client = CreateClient)
                        {
                            response = await client.SendAsync(request);
                        }

                } while (!response.IsSuccessStatusCode && retryCount++ < RetryCount);

                if (!response.IsSuccessStatusCode) throw new Exception("Ошибка получения данных");

                return await response.Content.ReadAsStringAsync();
        }

        #endregion

    }
}
