using Application.Interfaces.InterfaceServices.Language;
using Application.ViewModels.LanguageDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Language
{
	public class LanguageController : BaseController
	{
		private readonly ILanguageService _languageService;
		public LanguageController(ILanguageService languageService)
		{
			_languageService = languageService;
		}

		[HttpGet]
		public async Task<IActionResult> GetLanguageList()
		{
			var result = await _languageService.GetAllLanguagesAsync();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetLanguageById(Guid id)
		{
			var result = await _languageService.GetLanguageByIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> CreateLanguage([FromBody] CULanguageDTO cudLanguageDTO)
		{
			var result = await _languageService.CreateLanguageAsync(cudLanguageDTO);
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
		public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] CULanguageDTO cudLanguageDTO)
		{
			var result = await _languageService.UpdateLanguageAsync(id, cudLanguageDTO);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccount(Guid id)
		{
			var result = await _languageService.DeleteLanguageAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}


	}
}
