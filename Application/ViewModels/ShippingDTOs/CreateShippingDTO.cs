using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ShippingDTOs
{
	public class CreateShippingDTO
	{
		public Guid ShipperId { get; set; }
		public Guid OrderId { get; set; }
		public string? ImageUrl { get; set; }
	}
}
