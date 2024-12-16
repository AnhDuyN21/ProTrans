using Application.Interfaces.InterfaceServices.Orders;
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

		[HttpGet("GetCompletedOrders")]
		public async Task<IActionResult> GetCompletedOrders()
		{
			var result = await orderService.GetCompletedOrdersAsync();
			return Ok(result);
		}

		[HttpGet("GetOfflineOrders")]
		public async Task<IActionResult> GetOfflineOrders()
		{
			var result = await orderService.GetOfflineOrdersAsync();
			return Ok(result);
		}

		[HttpGet("GetOnlineOrders")]
		public async Task<IActionResult> GetOnlineOrders()
		{
			var result = await orderService.GetOnlineOrdersAsync();
			return Ok(result);
		}

		[HttpGet("GetOrdersToPickUp")]
		public async Task<IActionResult> GetOrdersToPickUp()
		{
			var result = await orderService.GetOrdersToPickUpAsync();
			return Ok(result);
		}

		[HttpGet("GetOrdersToReceive")]
		public async Task<IActionResult> GetOrdersToReceive()
		{
			var result = await orderService.GetOrdersToReceiveAsync();
			return Ok(result);
		}

		[HttpGet("GetCompletedOrdersByAgencyId")]
		public async Task<IActionResult> GetCompletedOrdersByAgencyId(Guid id)
		{
			var result = await orderService.GetCompletedOrdersByAgencyIdAsync(id);
			return Ok(result);
		}

		[HttpGet("GetByCustomerId")]
		public async Task<IActionResult> GetOrdersByCustomerId(Guid id)
		{
			var result = await orderService.GetOrdersByCustomerIdAsync(id);
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

		[HttpGet("GetByPhoneNumber")]
		public async Task<IActionResult> GetOrdersByPhoneNumber(string num)
		{
			var result = await orderService.GetOrdersByPhoneNumberAsync(num);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO order)
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

		[HttpPost("CreateOrderFromRequest")]
		public async Task<IActionResult> CreateOrderFromRequest([FromBody] Guid requestId)
		{
			var result = await orderService.CreateOrderFromRequestAsync(requestId);
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
		public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDTO CUorderDTO)
		{
			var result = await orderService.UpdateOrderAsync(id, CUorderDTO);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPut("UpdateToPickedUp/{id}")]
		public async Task<IActionResult> UpdateOrderToPickedUp(Guid id)
		{
			var result = await orderService.UpdateOrderToPickedUpAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPut("UpdateOrderStatus")]
		public async Task<IActionResult> UpdateOrderStatus(Guid id, string status)
		{
			var result = await orderService.UpdateOrderStatusAsync(id, status);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(Guid id, string reason)
		{
			var result = await orderService.DeleteOrderAsync(id, reason);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
	}
}