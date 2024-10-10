using Application.Commons;
using Application.ViewModels.PaymentMethodDTOs;

namespace Application.Interfaces.InterfaceServices.PaymentMethods
{
    public interface IPaymentMethodService
    {
        public Task<ServiceResponse<IEnumerable<PaymentMethodDTO>>> GetAllPaymentMethodsAsync();
        public Task<ServiceResponse<PaymentMethodDTO>> GetPaymentMethodByIdAsync(Guid id);
        public Task<ServiceResponse<PaymentMethodDTO>> UpdatePaymentMethodAsync(Guid id, CUPaymentMethodDTO paymentMethod);
        public Task<ServiceResponse<PaymentMethodDTO>> CreatePaymentMethodAsync(CUPaymentMethodDTO paymentMethod);
        public Task<ServiceResponse<bool>> DeletePaymentMethodAsync(Guid id);
    }
}