using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.ShippingDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.DocumentValidations
{
	public class DocumentValidation : AbstractValidator<UpdateDocumentDTO>
	{
		public DocumentValidation()
		{
			RuleFor(x => x.TranslationStatus)
				.Must(value => value == null || Enum.IsDefined(typeof(DocumentStatus), value))
				.WithMessage("The status must be Preparing, Shipping or Completed.");

			RuleFor(x => x.NotarizationStatus)
				.Must(value => value == null || Enum.IsDefined(typeof(DocumentStatus), value))
				.WithMessage("The status must be Preparing, Shipping or Completed.");
		}
	}
}
