﻿using Application.Interfaces.InterfaceServices.Account;
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

		[HttpGet]
		public async Task<IActionResult> GetAccountList()
		{
			var result = await _accountService.GetAccountAsync();
			return Ok(result);
		}

		[HttpGet("GetBy2LanguageId")]
		public async Task<IActionResult> GetTranslatorsBy2LanguageId(Guid firstlanguageId, Guid secondlanguageId, Guid documentId)
		{
			var result = await _accountService.GetTranslatorsBy2LanguageId(firstlanguageId, secondlanguageId, documentId);
			return Ok(result);
		}
		[HttpGet("GetByPhoneNumber")]
		public async Task<IActionResult> GetByPhoneNumber(string phoneNumber)
		{
			var result = await _accountService.GetAccountByPhoneNumberAsync(phoneNumber);
			return Ok(result);
		}

		[HttpGet("GetByAgencyId")]
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
		[HttpGet("GetAllShipperByAgencyId")]
		public async Task<IActionResult> GetAllShipperByAgencyId(Guid agencyId)
		{
			var result = await _accountService.GetShipperByAgencyIdAsync(agencyId);
			return Ok(result);
		}
		[HttpGet("GetAllTranslator")]
		public async Task<IActionResult> GetAllTranslator()
		{
			var result = await _accountService.GetTranslatorAsync();
			return Ok(result);
		}
		[HttpGet("GetAllStaff")]
		public async Task<IActionResult> GetAllStaff()
		{
			var result = await _accountService.GetStaffAsync();
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
		[HttpPost("Translator")]
		public async Task<IActionResult> CreateTranslator([FromBody] CreateTranslatorDTO createTranslatorDTO)
		{
			var result = await _accountService.CreateTranslatorAccountAsync(createTranslatorDTO);
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

		[HttpPut("Toggle")]
		public async Task<IActionResult> ToggleAccountStatus(Guid id)
		{
			var result = await _accountService.ToggleAccountStatusAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpGet("confirm")]
		public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
		{
			var result = await _accountService.ConfirmEmail(token);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

	}
}
