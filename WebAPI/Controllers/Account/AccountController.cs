using Application.Interfaces.InterfaceServices.Account;
using Application.ViewModels.AccountDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Account
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles ="Shipper")]
        [HttpGet]
        public async Task<IActionResult> GetAccountList()
        {
            var result = await _accountService.GetAccountAsync();
            return Ok(result);
        }

        [HttpGet("GetByLanguageId")]
        public async Task<IActionResult> GetTranslatorsByLanguageId(Guid id)
        {
            var result = await _accountService.GetTranslatorsByLanguageId(id);
            return Ok(result);
        }

        [HttpGet("GetByRoleId")]
        public async Task<IActionResult> GetAccountByAgencyId(Guid AgencyId)
        {
            var result = await _accountService.GetAccountByAgencyAsync(AgencyId);
            return Ok(result);
        }
        [HttpGet("GetAllShipper")]
        public async Task<IActionResult> GetAllShipper()
        {
            var result = await _accountService.GetShipperAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO createdAccountDTO)
        {
            var result = await _accountService.CreateAccountAsync(createdAccountDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] AccountDTO accountDTO)
        {
            var result = await _accountService.UpdateAccountAsync(id, accountDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }



        //[HttpPost("change-password/{userId}")]
        //public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordDTO changePasswordDto)
        //{
        //    var result = await _accountService.ChangePasswordAsync(userId, changePasswordDto);

        //    if (result.Success)
        //    {
        //        return Ok(new { Message = result.Message });
        //    }
        //    else
        //    {
        //        return BadRequest(new { Message = result.Message });
        //    }
        //}

        //[HttpGet("{name}")]
        //public async Task<IActionResult> SearchByName(string name)
        //{
        //    var result = await _accountService.SearchAccountByNameAsync(name);

        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}
        //[HttpGet("{role}")]
        //public async Task<IActionResult> SearchByRole([FromRoute] string role)
        //{
        //    var result = await _accountService.SearchAccountByRoleNameAsync(role);

        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }

        //}
        //[HttpGet]
        //public async Task<IActionResult> GetSortedAccount()
        //{
        //    var result = await _accountService.GetSortedAccountsAsync();

        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }

        //}
    }
}
