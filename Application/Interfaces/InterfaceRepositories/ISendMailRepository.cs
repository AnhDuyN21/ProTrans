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
        public byte[] CreatePDF(Domain.Entities.Order order, Domain.Entities.Document[] documents, Domain.Entities.DocumentPrice[] prices, string ShipperName, string shipperPhone, bool? pickupRequest);
        Task SendEmailWithPDFAsync(MessageDTO message, byte[] pdf, string imageURL, string customerName);
    }
}
