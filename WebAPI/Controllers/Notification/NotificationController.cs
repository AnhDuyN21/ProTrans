﻿using Application.Interfaces.InterfaceServices.NotarizationDetail;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationById(int id)
        {
            var result = await _notificationService.Update(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
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
            var roleName = await _notificationService.GetRoleStringAsync(sendNotificationDTO.SpecId);
            await _signalRHub.Clients.All.SendAsync(roleName, sendNotificationDTO.Title, sendNotificationDTO.Message, sendNotificationDTO.Author);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("Single")]
        public async Task<IActionResult> CreateANotification([FromBody] SendNotificationDTO sendNotificationDTO)
        {
            var result = await _notificationService.SendANotificationAsync(sendNotificationDTO);
         
            await _signalRHub.Clients.All.SendAsync(sendNotificationDTO.SpecId.ToString(), sendNotificationDTO.Title, sendNotificationDTO.Message, sendNotificationDTO.Author);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("SendLocation")]
        public async Task<IActionResult> SendLocation([FromBody] SendLocationDTO sendLocationDTO)
        {
          
            await _signalRHub.Clients.All.SendAsync(sendLocationDTO.SpecId.ToString(), sendLocationDTO.OrderId, sendLocationDTO.Latitude, sendLocationDTO.Longitude);
            return Ok();
        }

    }
}
