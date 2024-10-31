using Application.Commons;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.RoleDTOs;
namespace Application.Interfaces.InterfaceServices.Account
{
    public interface IRoleService
    {
        Task<ServiceResponse<IEnumerable<RoleDTO>>> GetRolesAsync();
 
        
    }
}
