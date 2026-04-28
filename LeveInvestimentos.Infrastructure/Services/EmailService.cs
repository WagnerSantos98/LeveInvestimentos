using System;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;

namespace LeveInvestimentos.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            // For MVP, we'll just log to console.
            // In a real application, implement SmtpClient or SendGrid here.
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"[EMAIL SIMULATION]");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("--------------------------------------------------");

            return Task.CompletedTask;
        }
    }
}
