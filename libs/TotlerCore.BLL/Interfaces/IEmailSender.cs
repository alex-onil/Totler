using System.Threading.Tasks;

namespace TotlerCore.BLL.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
