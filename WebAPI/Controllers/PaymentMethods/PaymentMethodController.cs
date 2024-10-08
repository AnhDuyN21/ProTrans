using Application.Interfaces.InterfaceServices.PaymentMethods;
using Application.ViewModels.PaymentMethodDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.PaymentMethods
{
    public class PaymentMethodController : BaseController
    {
        private readonly IPaymentMethodService paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            this.paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var result = await paymentMethodService.GetAllPaymentMethodsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodById(Guid id)
        {
            var result = await paymentMethodService.GetPaymentMethodByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] CUPaymentMethodDTO paymentMethod)
        {
            var result = await paymentMethodService.CreatePaymentMethodAsync(paymentMethod);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentMethod(Guid id, [FromBody] CUPaymentMethodDTO CUpaymentMethodDTO)
        {
            var result = await paymentMethodService.UpdatePaymentMethodAsync(id, CUpaymentMethodDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            var result = await paymentMethodService.DeletePaymentMethodAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
