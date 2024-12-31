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

			if (getById == null)
			{
				response.Success = false;
				response.Message = "Id is not existed";
			}
			else
			{
				response.Success = true;
				response.Message = "Agency found";
				response.Data = _mapper.Map<DistanceDTO>(getById);
			}

			return response;
		}
	}
}
