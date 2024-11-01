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

        [HttpGet("ShipperAndStaff")]
        public async Task<IActionResult> GetShipperAndStaff()
        {
            var result = await _RoleService.GetRolesShipperAndStaffAsync();
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _RoleService.GetAllRolesAsync();
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }



    }
}
