using System.Threading.Tasks;
using ClothingShop.Entity.Models;

namespace ClothingShop.BusinessLogic.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}