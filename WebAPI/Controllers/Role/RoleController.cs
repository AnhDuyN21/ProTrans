using Application.Interfaces.InterfaceServices.Account;

using Application.ViewModels.RoleDTOs;
using Infrastructures.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Controllers.Role
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _RoleService;
        private readonly IHubContext<SignalRHub> _signalRHub;
        public RoleController(IRoleService RoleService, IHubContext<SignalRHub> signalRHub)
        {
            _RoleService = RoleService;
            _signalRHub = signalRHub;

        }

        [HttpGet()]
        public async Task<IActionResult> GetRole()
        {
            var result = await _RoleService.GetRolesAsync();
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
     


    }
}
