﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.NotarizationDetail;
using Application.ViewModels.AssignmentNotarizationDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.NotarizationDetail
{
    public class NotarizationDetailService : INotarizationDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotarizationDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

       
    }
}