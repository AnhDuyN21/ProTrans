using Application.ViewModels.DocumentDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.DocumentValidations
{
	public class DocumentValidation : AbstractValidator<UpdateDocumentDTO>
	{
		public DocumentValidation()
		{
			RuleFor(x => x.TranslationStatus)
				.Must(value => value == null || Enum.IsDefined(typeof(DocumentTranslationStatus), value))
				.WithMessage("The status must be Processing, Translating or Translated.");

			RuleFor(x => x.NotarizationStatus)
				.Must(value => value == null || Enum.IsDefined(typeof(DocumentNotarizationStatus), value))
				.WithMessage("The status must be None, Processing, Notarizating, Notarizated.");
		}
	}
}
