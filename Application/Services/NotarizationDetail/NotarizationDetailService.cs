using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.NotarizationDetail;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AssignmentNotarizationDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Data.Common;

namespace Application.Services.NotarizationDetail
{
    public class NotarizationDetailService : INotarizationDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        public NotarizationDetailService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
        }

        public async Task<ServiceResponse<IEnumerable<NotarizationDetailDTO>>> GetAllNotarizationDetails(Guid Id)
        {
            var response = new ServiceResponse<IEnumerable<NotarizationDetailDTO>>();

            try
            {
                var notarizationDetailList = await _unitOfWork.NotarizationDetailRepository.GetAllAsync(a => a.AssignmentNotarizationId.Equals(Id) && a.IsDeleted.Equals(false));
                var notarizationDetailDTOs = _mapper.Map<List<NotarizationDetailDTO>>(notarizationDetailList);

                if (notarizationDetailDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "NotarizationDetail list retrieved successfully";
                    response.Data = notarizationDetailDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have NotarizationDetail in list";
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
        public async Task<ServiceResponse<IEnumerable<NotarizationDetailDTO>>> UpdateAllNotarizationDetailsByTaskId(Guid Id)
        {
            var response = new ServiceResponse<IEnumerable<NotarizationDetailDTO>>();
            bool CheckIfAllDocumentIsNotarized = true;
            try
            {
                var notarizationDetailList = await _unitOfWork.NotarizationDetailRepository.GetAllAsync(a => a.AssignmentNotarizationId.Equals(Id) && a.IsDeleted.Equals(false));
                var notarizationDetailDTOs = _mapper.Map<List<NotarizationDetailDTO>>(notarizationDetailList);

                List<string> id = new List<string>();
                foreach (var item in notarizationDetailList)
                {
                    var document = await _unitOfWork.DocumentRepository.GetByIdAsync(item.DocumentId);
                    document.NotarizationStatus = DocumentNotarizationStatus.Notarizated.ToString();

                    //Thêm thời gian cập nhật trạng thái document vào bảng document status
                    var documentStatus = new DocumentStatus
                    {
                        DocumentId = document.Id,
                        Status = DocumentNotarizationStatus.Notarizated.ToString(),
                        Type = TypeStatus.Notarization.ToString(),
                        Time = _currentTime.GetCurrentTime()
                    };
                    await _unitOfWork.DocumentStatusRepository.AddAsync(documentStatus);
                    await _unitOfWork.SaveChangeAsync();

                    _unitOfWork.DocumentRepository.Update(document);
                    id.Add(document.OrderId.ToString());
                }
                await _unitOfWork.SaveChangeAsync();

                foreach (var orderId in id)
                {
                    Guid id1 = Guid.Parse(orderId.ToString());
                    var Order = await _unitOfWork.OrderRepository.GetByIdAsync(id1);
                    var docList = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(id1);
                    foreach (var item in docList)
                    {
                        if (item.NotarizationStatus != DocumentNotarizationStatus.Notarizated.ToString())
                        {
                            CheckIfAllDocumentIsNotarized = false;
                        }

                    }

                    if (CheckIfAllDocumentIsNotarized)
                    {

                        Order.Status = OrderStatus.Completed.ToString();
                        _unitOfWork.OrderRepository.Update(Order);

                    }
                    await _unitOfWork.SaveChangeAsync();
                }
                if (notarizationDetailDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "NotarizationDetail list retrieved successfully";
                    response.Data = notarizationDetailDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have NotarizationDetail in list";
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

    }
}
