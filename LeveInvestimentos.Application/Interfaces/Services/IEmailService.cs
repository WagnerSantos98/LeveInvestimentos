using System.Threading.Tasks;

namespace LeveInvestimentos.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}