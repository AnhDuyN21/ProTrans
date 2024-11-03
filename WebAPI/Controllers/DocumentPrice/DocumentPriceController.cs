using Application.Interfaces.InterfaceServices.Documents;
using Application.Services.Documents;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DocumentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.DocumentPrice
{

    public class DocumentPriceController : BaseController
    {
        private readonly IDocumentService service;
        public DocumentPriceController(IDocumentService documentService)
        {
            service = documentService;
        }
        [HttpGet("ByDocument/{documentId}")]
        public async Task<IActionResult> GetByDocumentId(Guid documentId)
        {
            var result = await service.GetDocumentPriceByDocumentId(documentId);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDocumentPrice([FromBody] CreateDocumentPriceDTO createDocumentPriceDTO)
        {
            var result = await service.CreateDocumentPriceAsync(createDocumentPriceDTO);
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
        public async Task<IActionResult> UpdateDocumentPriceByDocumentId(Guid id, [FromBody] UpdateDocumentPriceDTO updateDocumentPriceDTO)
        {
            var result = await service.UpdateDocumentPriceAsync(id, updateDocumentPriceDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
