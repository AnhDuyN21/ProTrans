using Application.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            var idString = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");

            // Chuyển đổi từ string sang Guid
            if (Guid.TryParse(idString, out var id))
            {
                GetCurrentUserId = id;
            }
            else
            {
                GetCurrentUserId = Guid.Empty;
            }
        }

        public Guid GetCurrentUserId { get; }

    }
}
