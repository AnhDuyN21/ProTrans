using Application.Interfaces.InterfaceServices.Orders;
using Application.Services.Orders;
using Application.ViewModels.OrderDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Orders
{
	public class OrderController : BaseController
	{
		private readonly IOrderService orderService;
		public OrderController(IOrderService orderService)
		{
			this.orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
			var result = await orderService.GetAllOrdersAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(Guid id)
		{
			var result = await orderService.GetOrderByIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] CUOrderDTO order)
		{
			var result = await orderService.CreateOrderAsync(order);
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
		public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] CUOrderDTO CUorderDTO)
		{
			var result = await orderService.UpdateOrderAsync(id, CUorderDTO);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(Guid id)
		{
			var result = await orderService.DeleteOrderAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
	}
}
