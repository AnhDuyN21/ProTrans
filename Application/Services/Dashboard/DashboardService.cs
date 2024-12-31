using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Dashboard;
using Application.ViewModels.Dashboard;
using Application.ViewModels.DistanceDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Dashboard
{
	public class DashboardService : IDashboardService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<DashboardDTO>> Get()
		{
			var response = new ServiceResponse<DashboardDTO>();
			var dashboard = new DashboardDTO();
			var numberOfAccounts = await _unitOfWork.AccountRepository.GetCountAsync();
			dashboard.NumberOfAccounts = numberOfAccounts;

			response.Success = true;
			response.Message = "Get successfully.";
			response.Data = dashboard;

			return response;
		}
	}
}
