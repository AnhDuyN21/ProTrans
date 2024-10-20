using Application.Commons;
using Application.ViewModels.ShippingDTOs;

namespace Application.Interfaces.InterfaceServices.Shippings
{
    public interface IShippingService
    {
        public Task<ServiceResponse<IEnumerable<ShippingDTO>>> GetAllShippingsAsync();
        public Task<ServiceResponse<ShippingDTO>> GetShippingByIdAsync(Guid id);
        public Task<ServiceResponse<ShippingDTO>> UpdateShippingAsync(Guid id, UpdateShippingDTO shipping);
        public Task<ServiceResponse<ShippingDTO>> CreateShippingAsync(CreateShippingDTO shipping);
        public Task<ServiceResponse<bool>> DeleteShippingAsync(Guid id);
        public Task<ServiceResponse<IEnumerable<ShippingDTO>>> GetShippingsByShipperIdAsync(Guid id);
    }
}
