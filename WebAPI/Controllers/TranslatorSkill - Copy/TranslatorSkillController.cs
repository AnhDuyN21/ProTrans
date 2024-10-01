using Application.Interfaces.InterfaceServices.TranslatorSkill;
using Application.ViewModels.TranslatorSkillDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.TranslatorSkill
{
    public class TranslatorSkillController : BaseController
    {
        private readonly ITranslatorSkillService _translatorSkillService;
        public TranslatorSkillController(ITranslatorSkillService TranslatorSkillService)
        {
            _translatorSkillService = TranslatorSkillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTranslatorSkillList()
        {
            var result = await _translatorSkillService.GetAllTranslatorSkillsAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslatorSkillById(Guid id)
        {
            var result = await _translatorSkillService.GetTranslatorSkillByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTranslatorSkill([FromBody] CUTranslatorSkillDTO createdTranslatorSkillDTO)
        {
            var result = await _translatorSkillService.CreateTranslatorSkillAsync(createdTranslatorSkillDTO);
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
        public async Task<IActionResult> UpdateTranslatorSkill(Guid id, [FromBody] CUTranslatorSkillDTO cudTranslatorSkillDTO)
        {
            var result = await _translatorSkillService.UpdateTranslatorSkillAsync(id, cudTranslatorSkillDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatorSkill(Guid id)
        {
            var result = await _translatorSkillService.DeleteTranslatorSkillAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


    }
}
