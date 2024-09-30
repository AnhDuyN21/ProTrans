using Application.Interfaces.InterfaceServices.Notification;
using Application.ViewModels.NotificationDTOs;
using Infrastructures.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Controllers.Notification
{
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;
        private readonly IHubContext<SignalRHub> _signalRHub;
        public NotificationController(INotificationService notificationService, IHubContext<SignalRHub> signalRHub)
        {
            _notificationService = notificationService;
            _signalRHub = signalRHub;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            var result = await _notificationService.GetAllNotificationAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateNotification([FromBody] SendNotificationDTO sendNotificationDTO)
        {
            var result = await _notificationService.SendNotificationAsync(sendNotificationDTO);
            var roleName = await _notificationService.GetRoleStringAsync(sendNotificationDTO.RoleId);
            await _signalRHub.Clients.All.SendAsync(roleName, sendNotificationDTO.Title, sendNotificationDTO.Author, sendNotificationDTO.Message);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


    }
}
