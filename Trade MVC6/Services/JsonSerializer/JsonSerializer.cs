using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Trade_MVC6.Services.JsonSerializer
{
    public class SimpleSerializer : IJsonSerializer
    {
        public Task<T> ParseAsync<T>(string value) => 
             Task.Run(() => JsonConvert.DeserializeObject<T>(value));

        public Task<string> StringifyAsync(object value) =>
            Task.Run(() => JsonConvert.SerializeObject(value));
    }
}
