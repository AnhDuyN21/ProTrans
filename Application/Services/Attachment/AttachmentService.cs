using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Attachment;
using Application.Interfaces.InterfaceServices.Image;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AttachmentDTOs;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<ServiceResponse<IEnumerable<AttachmentDTO>>> GetAttachmentAsync()
        {
            var response = new ServiceResponse<IEnumerable<AttachmentDTO>>();

            try
            {
                var attachmentList = await _unitOfWork.AttachmentRepository.GetAllAsync();
                var attachmentDTOs = _mapper.Map<List<AttachmentDTO>>(attachmentList);

                if (attachmentDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Attachment list retrieved successfully";
                    response.Data = attachmentDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have attachment in list";
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
        public async Task<ServiceResponse<AttachmentDTO>> GetAttachmentByIdAsync(Guid id)
        {
            var response = new ServiceResponse<AttachmentDTO>();
            var attachmentGetById = await _unitOfWork.AttachmentRepository.GetByIdAsync(id);
            if (attachmentGetById == null)
            {
                response.Success = false;
                response.Message = "Attachment is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Account found";
                response.Data = _mapper.Map<AttachmentDTO>(attachmentGetById);
            }
            return response;
        }
        public async Task<ServiceResponse<AttachmentDTO>> CreateAttachmentAsync(Guid requestId ,CreateAttachmentDTO createAttachmentDTO)
        {
            var response = new ServiceResponse<AttachmentDTO>();
            try
            {
                var attachment = _mapper.Map<Domain.Entities.Attachment>(createAttachmentDTO);
                attachment.RequestId = requestId;
                await _unitOfWork.AttachmentRepository.AddAsync(attachment);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (!createAttachmentDTO.AttachmentImages.IsNullOrEmpty())
                {
                    await _imageService.UploadAttachmentImages(createAttachmentDTO.AttachmentImages, attachment.Id);
                }
                if (isSuccess)
                {
                    var attachmentDTO = _mapper.Map<AttachmentDTO>(attachment);
                    response.Data = attachmentDTO;
                    response.Success = true;
                    response.Message = "Attachment created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the attachment.";
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
        public async Task<ServiceResponse<AttachmentDTO>> UpdateAttachmentAsync(Guid id, CreateAttachmentDTO createAttachmentDTO)
        {
            var response = new ServiceResponse<AttachmentDTO>();

            try
            {
                var attachmentGetById = await _unitOfWork.AttachmentRepository.GetByIdAsync(id);
                if (attachmentGetById == null)
                {
                    response.Success = false;
                    response.Message = "Attachment Id not found.";
                    return response;
                }
                var objectToUpdate = _mapper.Map(createAttachmentDTO, attachmentGetById);
                _unitOfWork.AttachmentRepository.Update(attachmentGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<AttachmentDTO>(objectToUpdate);
                    response.Success = true;
                    response.Message = "Attachment updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the attachment.";
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
        public async Task<ServiceResponse<bool>> DeleteAttachmentAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var attachmentGetById = await _unitOfWork.AttachmentRepository.GetByIdAsync(id);
            if (attachmentGetById == null)
            {
                response.Success = false;
                response.Message = "Attachment ID is not found";
                return response;
            }

            try
            {
                _unitOfWork.AttachmentRepository.SoftRemove(attachmentGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Attachment deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the attachment.";
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
