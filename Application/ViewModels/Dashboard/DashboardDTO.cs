using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Dashboard
{
	public class DashboardDTO
	{
		public int? NumberOfAccounts { get; set; } = 0;
		public int? NumberOfOrders { get; set; } = 0;
		public decimal? Revenue { get; set; } = 0;
	}
}
