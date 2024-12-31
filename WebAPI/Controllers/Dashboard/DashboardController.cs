using Application.Interfaces.InterfaceServices.Dashboard;
using Application.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Dashboard
{
	public class DashboardController : BaseController
	{
		private readonly IDashboardService dashboardService;
		public DashboardController(IDashboardService dashboardService)
		{
			this.dashboardService = dashboardService;
		}

		[HttpGet]
		public async Task<IActionResult> GetDashboard([FromBody] DashboardTimeDTO time)
		{
			var result = await dashboardService.Get(time);
			return Ok(result);
		}
	}
}
