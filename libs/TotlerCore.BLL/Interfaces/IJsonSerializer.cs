using System.Threading.Tasks;

namespace TotlerCore.BLL.Interfaces
{
    public interface IJsonSerializer
    {
        Task<T> ParseAsync<T>(string value);
        Task<string> StringifyAsync(object value);
    }
}
