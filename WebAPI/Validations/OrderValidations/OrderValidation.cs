using Application.ViewModels.OrderDTOs;
using Application.ViewModels.ShippingDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.OrderValidations
{
	public class OrderValidation : AbstractValidator<UpdateOrderDTO>
	{
		public OrderValidation()
		{
			RuleFor(x => x.Status)
				.Must(value => Enum.IsDefined(typeof(OrderStatus), value))
				.WithMessage("The status must be Preparing, Shipping or Completed.");
		}
	}
}
