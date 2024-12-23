using Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.VNPay
{
    public interface IVNPayService
    {
        Task<ServiceResponse<string>> CreatePaymentlink(Guid orderId, decimal totalPrice);
        Task<ServiceResponse<string>> VerifyPayment(string url);

    }
}
