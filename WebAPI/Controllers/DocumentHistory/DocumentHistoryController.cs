using Application.Interfaces.InterfaceServices.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.DocumentHistory
{

    public class DocumentHistoryController : BaseController
    {
        private readonly IDocumentService documentService;
        public DocumentHistoryController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }
        [HttpGet("ByDocument/{documentId}")]
        public async Task<IActionResult> GetByDocumentId(Guid documentId)
        {
            var result = await documentService.GetDocumentHistoryByDocumentIdAsync(documentId);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await documentService.GetDocumentHistoryByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
