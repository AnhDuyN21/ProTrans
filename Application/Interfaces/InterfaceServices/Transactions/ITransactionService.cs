using Application.Commons;
using Application.ViewModels.TransactionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Transactions
{
	public interface ITransactionService
	{
		public Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetAllTransactionsAsync();
		public Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(Guid id);
		public Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(Guid id, CUTransactionDTO transaction);
		public Task<ServiceResponse<TransactionDTO>> CreateTransactionAsync(CUTransactionDTO transaction);
		public Task<ServiceResponse<bool>> DeleteTransactionAsync(Guid id);
	}
}