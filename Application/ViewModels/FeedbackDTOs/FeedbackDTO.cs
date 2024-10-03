using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FeedbackDTOs
{
	public class FeedbackDTO
	{
		public Guid Id { get; set; }
		public string? Message { get; set; }
		public Guid AccountId { get; set; }
		public Guid OrderId { get; set; }
	}
}