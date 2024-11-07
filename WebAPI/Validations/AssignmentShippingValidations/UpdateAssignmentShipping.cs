using Application.ViewModels.AssignmentShippingDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.AssignmentShippingValidations
{
	public class UpdateAssignmentShipping : AbstractValidator<UpdateAssignmentShippingDTO>
	{
		public UpdateAssignmentShipping()
		{
			RuleFor(x => x.Status)
				.Must(value => Enum.IsDefined(typeof(AssignmentShippingStatus), value))
				.WithMessage("The status must be Preparing, Shipping or Completed.");
		}
	}
}
