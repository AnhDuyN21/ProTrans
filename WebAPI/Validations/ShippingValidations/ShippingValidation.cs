using Application.ViewModels.ShippingDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.ShippingValidations
{
	public class ShippingValidation : AbstractValidator<CUShippingDTO>
	{
		public ShippingValidation()
		{
			RuleFor(x => x.Status)
				.Must(value => Enum.IsDefined(typeof(ShippingStatus), value))
				.WithMessage("The status must be Preparing, Shipping or Completed.");
		}
	}
}
