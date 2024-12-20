﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.QuotePrice;
using Application.ViewModels.QuotePriceDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.QuotePrice
{
    public class QuotePriceService : IQuotePriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuotePriceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<QuotePriceDTO>> CreateQuotePriceAsync(CUQuotePriceDTO createQuotePriceDTO)
        {

            var response = new ServiceResponse<QuotePriceDTO>();
            try
            {
                var firstLanguage = await _unitOfWork.LanguageRepository.GetByIdAsync((Guid)createQuotePriceDTO.FirstLanguageId);
                var secondLanguage = await _unitOfWork.LanguageRepository.GetByIdAsync((Guid)createQuotePriceDTO.SecondLanguageId);
                if (firstLanguage == null || secondLanguage == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy ngôn ngữ.";
                    return response;
                }
                var list = await _unitOfWork.QuotePriceRepository.GetAllAsync(x => x.FirstLanguageId == createQuotePriceDTO.FirstLanguageId);
                foreach (var item in list)
                {
                    if (item.SecondLanguageId == createQuotePriceDTO.SecondLanguageId)
                    {
                        response.Success = false;
                        response.Message = $"Đã tồn tại bảng giá dịch từ ngôn ngữ {firstLanguage.Name} sang {secondLanguage.Name}.";
                        return response;
                    }
                }
                var quotePrice = _mapper.Map<Domain.Entities.QuotePrice>(createQuotePriceDTO);


                await _unitOfWork.QuotePriceRepository.AddAsync(quotePrice);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var quotePriceDTO = _mapper.Map<QuotePriceDTO>(quotePrice);
                    response.Data = quotePriceDTO; // Chuyển đổi sang QuotePriceDTO
                    response.Success = true;
                    response.Message = "Quote Price created successfully.";
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

        public async Task<ServiceResponse<bool>> DeleteQuotePriceAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var QuotePriceGetById = await _unitOfWork.QuotePriceRepository.GetByIdAsync(id);
            if (QuotePriceGetById == null)
            {
                response.Success = false;
                response.Message = "QuotePrice is not existed";
                return response;
            }

            try
            {
                _unitOfWork.QuotePriceRepository.SoftRemove(QuotePriceGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "QuotePrice deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the QuotePrice.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<QuotePriceDTO>>> GetAllQuotePricesAsync()
        {
            var response = new ServiceResponse<IEnumerable<QuotePriceDTO>>();

            try
            {
                var quotePriceList = await _unitOfWork.QuotePriceRepository.GetAllAsync(x => x.IsDeleted == false);
                var quotePriceDTOs = _mapper.Map<List<QuotePriceDTO>>(quotePriceList);

                if (quotePriceDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Quote Price list retrieved successfully";
                    response.Data = quotePriceDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Quote Price in list";
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

        public async Task<ServiceResponse<QuotePriceDTO>> GetQuotePriceByIdAsync(Guid Id)
        {
            var response = new ServiceResponse<QuotePriceDTO>();

            try
            {
                var quotePriceById = await _unitOfWork.QuotePriceRepository.GetByIdAsync(Id);
                var quotePriceDTOs = _mapper.Map<QuotePriceDTO>(quotePriceById);

                if (quotePriceById == null)
                {
                    response.Success = false;
                    response.Message = "Quote Price retrieved successfully";

                }
                else
                {
                    response.Success = true;
                    response.Message = "Quote Price not found";
                    response.Data = quotePriceDTOs;
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

        public async Task<ServiceResponse<QuotePriceDTO>> UpdateQuotePriceAsync(Guid id, CUQuotePriceDTO cuQuotePriceDTO)
        {

            var response = new ServiceResponse<QuotePriceDTO>();

            try
            {
                var QuotePriceGetById = await _unitOfWork.QuotePriceRepository.GetByIdAsync(id);

                if (QuotePriceGetById == null)
                {
                    response.Success = false;
                    response.Message = "QuotePrice not found.";
                    return response;
                }
                if ((bool)QuotePriceGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "QuotePrice is deleted in system";
                    return response;
                }
                // Map QuotePriceDT0 => existingUser
                var objectToUpdate = _mapper.Map(cuQuotePriceDTO, QuotePriceGetById);


                _unitOfWork.QuotePriceRepository.Update(QuotePriceGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<QuotePriceDTO>(objectToUpdate);
                    response.Success = true;
                    response.Message = "QuotePrice updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the QuotePrice.";
                }
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
