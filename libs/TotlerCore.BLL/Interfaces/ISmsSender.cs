using System.Threading.Tasks;

namespace TotlerCore.BLL.Interfaces
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
