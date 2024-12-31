using Application.Interfaces.InterfaceServices.Dashboard;
using Application.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "Manager")]
		public async Task<IActionResult> GetDashboard(DateTime fromTime, DateTime toTime)
		{
			var result = await dashboardService.Get(fromTime, toTime);
			return Ok(result);
		}
	}
}
