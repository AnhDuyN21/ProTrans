using Application.Interfaces.InterfaceServices.Account;
using Application.ViewModels.AccountDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Authentication
{

    public class AuthenticationController : BaseController
    {
        private readonly IAccountService _accountService;
        public AuthenticationController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTO registerDTO)
        {
            var result = await _accountService.RegisterAsync(registerDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
