using Application.Commons;
using Application.ViewModels.AgencyDTOs;
using Application.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Dashboard
{
	public interface IDashboardService
	{
		Task<ServiceResponse<DashboardDTO>> Get(DateTime fromTime, DateTime toTime);
	}
}
