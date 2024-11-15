using Application.Commons;
using Application.ViewModels.AssignmentShippingDTOs;

namespace Application.Interfaces.InterfaceServices.AssignmentShippings
{
	public interface IAssignmentShippingService
	{
		public Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetAllAssignmentShippingsAsync();
		public Task<ServiceResponse<AssignmentShippingDTO>> GetAssignmentShippingByIdAsync(Guid id);
		public Task<ServiceResponse<AssignmentShippingDTO>> UpdateAssignmentShippingAsync(Guid id, UpdateAssignmentShippingDTO assignmentShipping);
		public Task<ServiceResponse<AssignmentShippingDTO>> UpdateAssignmentShippingToCompletedAsync(Guid id);
		//public Task<ServiceResponse<AssignmentShippingDTO>> CreateAssignmentShippingAsync(CreateAssignmentShippingDTO assignmentShipping);
		public Task<ServiceResponse<AssignmentShippingDTO>> CreateAssignmentShippingToShipAsync(CreateAssignmentShippingDTO assignmentShipping);
		public Task<ServiceResponse<AssignmentShippingDTO>> CreateAssignmentShippingToPickUpAsync(CreateAssignmentShippingDTO assignmentShipping);
		public Task<ServiceResponse<bool>> DeleteAssignmentShippingAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetAssignmentShippingsByShipperIdAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetShipAssignmentShippingsByShipperIdAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetPickUpAssignmentShippingsByShipperIdAsync(Guid id);
	}
}
