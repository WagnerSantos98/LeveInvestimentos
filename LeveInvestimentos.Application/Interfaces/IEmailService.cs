using System.Threading.Tasks;

namespace LeveInvestimentos.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
