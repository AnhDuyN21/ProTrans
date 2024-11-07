using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ImageShippingDTOs
{
	public class ImageShippingDTO
	{
		public Guid Id { get; set; }
		public string? UrlPath { get; set; }
		public Guid AssignmentShippingId { get; set; }
		public Guid DocumentId { get; set; }
	}
}
