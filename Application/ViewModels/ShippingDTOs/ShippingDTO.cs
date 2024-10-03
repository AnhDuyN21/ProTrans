using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ShippingDTOs
{
	public class ShippingDTO
	{
		public Guid Id { get; set; }
		public Guid ShipperId { get; set; }
		public Guid OrderId { get; set; }
		public string ImageUrl { get; set; }
		public bool IsShipped { get; set; }
	}
}