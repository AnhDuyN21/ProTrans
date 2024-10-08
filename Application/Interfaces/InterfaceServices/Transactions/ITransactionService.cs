using Application.Commons;
using Application.ViewModels.TransactionDTOs;

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