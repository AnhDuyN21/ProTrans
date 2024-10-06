using Application.Commons;
using Application.Interfaces.InterfaceServices.Notarization;
using Application.Interfaces;
using Application.ViewModels.NotarizationDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.InterfaceServices.DocumentType;
using Application.ViewModels.DocumentTypeDTOs;

namespace Application.Services.DocumentType
{
    public class DocumentTypeService  : IDocumentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DocumentTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<DocumentTypeDTO>>> GetDocumentTypeAsync()
        {
            var response = new ServiceResponse<IEnumerable<DocumentTypeDTO>>();

            try
            {
                var documentTypeList = await _unitOfWork.DocumentTypeRepository.GetAllAsync();
                var documentTypeDTOs = _mapper.Map<List<DocumentTypeDTO>>(documentTypeList);

                if (documentTypeDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "List retrieved successfully";
                    response.Data = documentTypeDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have data in list";
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
        public async Task<ServiceResponse<DocumentTypeDTO>> GetDocumentTypeByIdAsync(Guid id)
        {
            var response = new ServiceResponse<DocumentTypeDTO>();

            var documentTypeGetById = await _unitOfWork.DocumentTypeRepository.GetByIdAsync(id);
            if (documentTypeGetById == null)
            {
                response.Success = false;
                response.Message = "Id is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Notarization found";
                response.Data = _mapper.Map<DocumentTypeDTO>(documentTypeGetById);
            }
            return response;
        }
        public async Task<ServiceResponse<CUDocumentTypeDTO>> CreateDocumentTypeAsync(CUDocumentTypeDTO cUDocumentTypeDTO)
        {
            var response = new ServiceResponse<CUDocumentTypeDTO>();
            try
            {
                var documentType = _mapper.Map<Domain.Entities.DocumentType>(cUDocumentTypeDTO);
                await _unitOfWork.DocumentTypeRepository.AddAsync(documentType);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CUDocumentTypeDTO>(documentType);
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving data.";
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
        public async Task<ServiceResponse<CUDocumentTypeDTO>> UpdateDocumentTypeAsync(Guid id, CUDocumentTypeDTO cUDocumentTypeDTO)
        {
            var response = new ServiceResponse<CUDocumentTypeDTO>();
            try
            {
                var getDocumentTypeById = await _unitOfWork.DocumentTypeRepository.GetByIdAsync(id);
                if (getDocumentTypeById == null)
                {
                    response.Success = false;
                    response.Message = "Id not exist!";
                    return response;
                }
                var updatedDocumentType = _mapper.Map(cUDocumentTypeDTO, getDocumentTypeById);
                _unitOfWork.DocumentTypeRepository.Update(updatedDocumentType);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CUDocumentTypeDTO>(updatedDocumentType);
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving data.";
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
        public async Task<ServiceResponse<bool>> DeleteDocumentTypeAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var documentTypeGetById = await _unitOfWork.DocumentTypeRepository.GetByIdAsync(id);
            if (documentTypeGetById == null)
            {
                response.Success = false;
                response.Message = "Id is not exist";
                return response;
            }

            try
            {
                _unitOfWork.DocumentTypeRepository.SoftRemove(documentTypeGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete fail.";
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
