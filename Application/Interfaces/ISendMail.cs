using Application.Commons;
using Application.ViewModels.SendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISendMail
    {
        Task SendEmailAsync(MessageDTO messageDTO);
        public Task<ServiceResponse<bool>> SendBill(MessageDTO message, Guid orderId, string shipperName, string shipperPhone, string image);
    }
}
