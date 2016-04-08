using System.Threading.Tasks;

namespace Trade_MVC6.Services
{
    public interface IJsonSerializer
    {
        Task<T> ParseAsync<T>(string value);
        Task<string> StringifyAsync(object value);
    }
}
