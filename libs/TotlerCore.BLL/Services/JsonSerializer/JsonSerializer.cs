using System.Threading.Tasks;
using Newtonsoft.Json;
using TotlerCore.BLL.Interfaces;

namespace TotlerCore.BLL.Services.JsonSerializer
{
    public class SimpleSerializer : IJsonSerializer
    {
        public Task<T> ParseAsync<T>(string value) => 
             Task.Run(() => JsonConvert.DeserializeObject<T>(value));

        public Task<string> StringifyAsync(object value) =>
            Task.Run(() => JsonConvert.SerializeObject(value));
    }
}
