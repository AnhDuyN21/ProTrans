using Application.Interfaces.InterfaceServices.NotarizationDetail;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.NotarizationDetail
{
    public class NotarizationDetailController : BaseController
    {
        private readonly INotarizationDetailService _NotarizationDetailService;
        public NotarizationDetailController(INotarizationDetailService NotarizationDetailService)
        {
            _NotarizationDetailService = NotarizationDetailService;
        }

    
  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotarizationDetailById(Guid id)
        {
            var result = await _NotarizationDetailService.GetAllNotarizationDetails(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

  
    }
}
