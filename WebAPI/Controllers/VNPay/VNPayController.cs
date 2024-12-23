using Application.Interfaces.InterfaceServices.VNPay;
using Application.ViewModels.NotificationDTOs;
using Infrastructures.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Controllers.VNPays
{
    public class VNPayController : BaseController
    {
        private readonly IVNPayService VNPayService;
        private readonly IHubContext<SignalRHub> _signalRHub;
        public VNPayController(IVNPayService _VNPayService,IHubContext<SignalRHub> signalRHub)
        {
            VNPayService = _VNPayService;
            _signalRHub = signalRHub;
        }
        [HttpGet()]
        public async Task<IActionResult> GreatePaymentLink(Guid shipperid, decimal totalprice)
        {
            var result = await VNPayService.CreatePaymentlink( shipperid, totalprice);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPayment(string url)
        {
            var result = await VNPayService.VerifyPayment(url);
            if (result.Success)
            {
                await _signalRHub.Clients.All.SendAsync(result.Data, "200","Thanh toán thành công" );
                return Ok(result);
            }
            else
            {
                await _signalRHub.Clients.All.SendAsync(result.Data, "404", "Thanh toán thất bại");
                return BadRequest(result);
            }
        }
    }
}