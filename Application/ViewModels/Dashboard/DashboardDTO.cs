using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Dashboard
{
	public class DashboardDTO
	{
		public int? NumberOfAccounts { get; set; }
		public int? NumberOfOrders { get; set; }
		public int? DailyRevenue { get; set; }
		public int? WeeklyRevenue { get; set; }
		public int? MonthlyRevenue { get; set; }
		public int? AnnualRevenue { get; set; }
	}
}
