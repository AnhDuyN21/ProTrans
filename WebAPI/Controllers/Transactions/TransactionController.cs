using Application.Interfaces.InterfaceServices.Transactions;
using Application.ViewModels.TransactionDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Transactions
{
	public class TransactionController : BaseController
	{
		private readonly ITransactionService transactionService;
		public TransactionController(ITransactionService transactionService)
		{
			this.transactionService = transactionService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTransactions()
		{
			var result = await transactionService.GetAllTransactionsAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTransactionById(Guid id)
		{
			var result = await transactionService.GetTransactionByIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTransaction([FromBody] CUTransactionDTO transaction)
		{
			var result = await transactionService.CreateTransactionAsync(transaction);
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] CUTransactionDTO CUtransactionDTO)
		{
			var result = await transactionService.UpdateTransactionAsync(id, CUtransactionDTO);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTransaction(Guid id)
		{
			var result = await transactionService.DeleteTransactionAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
	}
}