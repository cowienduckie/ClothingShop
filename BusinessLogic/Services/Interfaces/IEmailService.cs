using ClothingShop.Entity.Models;
using System.Threading.Tasks;

namespace ClothingShop.BusinessLogic.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}