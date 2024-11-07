using Application.ViewModels.AssignmentShippingDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.AssignmentShippingValidations
{
	public class CreateAssignmentShipping : AbstractValidator<CreateAssignmentShippingDTO>
	{
		public CreateAssignmentShipping()
		{
			RuleFor(x => x.Type)
				.Must(value => Enum.IsDefined(typeof(AssignmentShippingType), value))
				.WithMessage("The type must be Ship or PickUp.");
		}
	}
}
