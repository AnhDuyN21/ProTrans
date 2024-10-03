using Application.Interfaces.InterfaceServices.Attachment;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AttachmentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Attachment
{
    public class AttachmentController : BaseController
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAttachmentList()
        {
            var result = await _attachmentService.GetAttachmentAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttachmentById(Guid id)
        {
            var result = await _attachmentService.GetAttachmentByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAttachment([FromForm] CreateAttachmentDTO createAttachmentDTO)
        {
            var result = await _attachmentService.CreateAttachmentAsync(createAttachmentDTO);
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
        public async Task<IActionResult> UpdateAttachment(Guid id, CreateAttachmentDTO createAttachmentDTO)
        {
            var result = await _attachmentService.UpdateAttachmentAsync(id, createAttachmentDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(Guid id)
        {
            var result = await _attachmentService.DeleteAttachmentAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
