using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.NotarizationDetail;
using Application.Interfaces.InterfaceServices.Notification;
using Application.ViewModels.NotificationDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<NotificationDTO>>> GetAllNotificationAsync(Guid Id)
        {
            var response = new ServiceResponse<IEnumerable<NotificationDTO>>();

            try
            {
                var notificationList = await _unitOfWork.NotificationRepository.GetAllNotificationAsync(a => a.AccountId.Equals(Id));
                var notificationDTOs = _mapper.Map<List<NotificationDTO>>(notificationList.Select(q => new NotificationDTO
                {
                    Title = q.Title,
                    Author = q.Author,
                    Message = q.Message,
                    NotificationTime = q.NotificationTime
                }));

                if (notificationDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Notification list retrieved successfully";
                    response.Data = notificationDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Notification in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }

        public async Task<string> GetRoleStringAsync(Guid RoleId)
        {
            var response = _unitOfWork.RoleRepository.GetRoleName(RoleId);
            var roleName = response;
            return roleName;
        }

        public async Task<ServiceResponse<NotificationDTO>> SendANotificationAsync(SendNotificationDTO sendNotificationDTO)
        {
            var response = new ServiceResponse<NotificationDTO>();
            try
            {
                var notification = _mapper.Map<Domain.Entities.Notification>(sendNotificationDTO);


                await _unitOfWork.NotificationRepository.SendANotificationAsync(notification, sendNotificationDTO.SpecId);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var notificationDTO = _mapper.Map<NotificationDTO>(notification);
                    response.Data = notificationDTO; // Chuyển đổi sang NotificationDTO
                    response.Success = true;
                    response.Message = "Send Notification successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    

        public async Task<ServiceResponse<NotificationDTO>> SendNotificationAsync(SendNotificationDTO sendNotificationDTO)
        {
            var response = new ServiceResponse<NotificationDTO>();
            try
            {
                var notification = _mapper.Map<Domain.Entities.Notification>(sendNotificationDTO);


                await _unitOfWork.NotificationRepository.SendNotificationAsync(notification, sendNotificationDTO.SpecId);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var notificationDTO = _mapper.Map<NotificationDTO>(notification);
                    response.Data = notificationDTO; // Chuyển đổi sang NotificationDTO
                    response.Success = true;
                    response.Message = "Send Notification successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
