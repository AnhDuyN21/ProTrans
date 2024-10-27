using Application.Interfaces.InterfaceServices.Documents;
using Application.ViewModels.DocumentDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Documents
{
    public class DocumentController : BaseController
    {
        private readonly IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var result = await documentService.GetAllDocumentsAsync();
            return Ok(result);
        }

		[HttpGet("GetDocumentsToBeNotarizated")]
		public async Task<IActionResult> GetDocumentsToBeNotarizated()
		{
			var result = await documentService.GetDocumentsToBeNotarizatedAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(Guid id)
        {
            var result = await documentService.GetDocumentByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentDTO Document)
        {
            var result = await documentService.CreateDocumentAsync(Document);
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
        public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] UpdateDocumentDTO CUdocumentDTO)
        {
            var result = await documentService.UpdateDocumentAsync(id, CUdocumentDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var result = await documentService.DeleteDocumentAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetByOrderId")]
        public async Task<IActionResult> GetDocumentsByOrderId(Guid id)
        {
            var result = await documentService.GetDocumentsByOrderIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
