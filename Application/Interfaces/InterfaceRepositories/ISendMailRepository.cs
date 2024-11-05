using Application.ViewModels.SendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories
{
    public interface ISendMailRepository
    {
        Task SendEmailAsync(MessageDTO message);
    }
}
