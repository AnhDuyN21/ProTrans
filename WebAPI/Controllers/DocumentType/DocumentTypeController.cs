using Application.Interfaces.InterfaceServices.DocumentType;
using Application.ViewModels.DocumentTypeDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.DocumentType
{

    public class DocumentTypeController : BaseController
    {
        private readonly IDocumentTypeService _service;
        public DocumentTypeController(IDocumentTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetDocumentTypeAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetDocumentTypeByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CUDocumentTypeDTO cUDocumentTypeDTO)
        {
            var result = await _service.CreateDocumentTypeAsync(cUDocumentTypeDTO);
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
        public async Task<IActionResult> Update(Guid id, CUDocumentTypeDTO cUDocumentTypeDTO)
        {
            var result = await _service.UpdateDocumentTypeAsync(id, cUDocumentTypeDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteDocumentTypeAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
