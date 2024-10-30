using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Account;
using Application.ViewModels.RoleDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.role
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<RoleDTO>>> GetRolesAsync()
        {
            var response = new ServiceResponse<IEnumerable<RoleDTO>>();

            try
            {
                var roleList = await _unitOfWork.RoleRepository.GetRoles(a => a.Name.Equals("Shipper") || a.Name.Equals("Staff"));
                var roleDTOs = _mapper.Map<List<RoleDTO>>(roleList);

                if (roleDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Role list retrieved successfully";
                    response.Data = roleDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have role in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
    
    }
}
