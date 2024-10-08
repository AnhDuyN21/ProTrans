using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Transactions;
using Application.ViewModels.TransactionDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Transactions
{
	public class TransactionService : ITransactionService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetAllTransactionsAsync()
		{
			var response = new ServiceResponse<IEnumerable<TransactionDTO>>();

			try
			{
				var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();
				var transactionDTOs = _mapper.Map<List<TransactionDTO>>(transactions);

				if (transactionDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = transactionDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No transaction exists.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}

		public async Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(Guid id)
		{
			var response = new ServiceResponse<TransactionDTO>();

			var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
			if (transaction == null)
			{
				response.Success = false;
				response.Message = "Transaction is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Transaction found.";
				response.Data = _mapper.Map<TransactionDTO>(transaction);
			}
			return response;
		}

		public async Task<ServiceResponse<TransactionDTO>> CreateTransactionAsync(CUTransactionDTO CUTransactionDTO)
		{
			var response = new ServiceResponse<TransactionDTO>();
			try
			{
				var transaction = _mapper.Map<Transaction>(CUTransactionDTO);

				await _unitOfWork.TransactionRepository.AddAsync(transaction);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

				if (isSuccess)
				{
					var TransactionDTO = _mapper.Map<TransactionDTO>(transaction);
					response.Data = TransactionDTO;
					response.Success = true;
					response.Message = "Create successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving.";
				}
			}
			catch (DbException ex)
			{
				response.Success = false;
				response.Message = "Database error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<ServiceResponse<bool>> DeleteTransactionAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
			if (transaction == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.TransactionRepository.Delete(transaction);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Delete successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error deleting.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(Guid id, CUTransactionDTO CUtransactionDTO)
		{
			var response = new ServiceResponse<TransactionDTO>();
			try
			{
				var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);

				if (transaction == null)
				{
					response.Success = false;
					response.Message = "Transaction is not existed.";
					return response;
				}
				var result = _mapper.Map(CUtransactionDTO, transaction);

				_unitOfWork.TransactionRepository.Update(transaction);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<TransactionDTO>(result);
					response.Success = true;
					response.Message = "Update successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error updating.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
	}
}