using Application.ViewModels.DocumentDTOs;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.OrderDTOs
{
	public class CreateOrderDTO
	{
		public Guid? RequestId { get; set; }
		public DateTime? Deadline { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public bool ShipRequest { get; set; }
		[Required(ErrorMessage = "Cần có ít nhất 1 tài liệu để tạo đơn hàng. Vui lòng kiểm tra lại.")]
		public List<CreateDocumentDTO> Documents { get; set; }
	}
}
