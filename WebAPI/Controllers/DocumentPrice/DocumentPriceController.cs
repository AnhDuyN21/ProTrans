using Application.Interfaces.InterfaceServices.Documents;
using Application.Services.Documents;
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
    }
}
