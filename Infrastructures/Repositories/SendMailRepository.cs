using Application.Interfaces.InterfaceRepositories;
using Application.ViewModels.SendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class SendMailRepository : ISendMailRepository
    {
        private readonly SmtpClient _smtpClient;

        public SendMailRepository(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(MessageDTO message)
        {
            var mailMessage = new MailMessage("duynguyenbt21093@gmail.com", message.To)
            {
                Subject = message.Subject,
                Body = $"<html><body><p>{message.Body}</p><img src='{message.ImageUrl}'/></body></html>",
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
