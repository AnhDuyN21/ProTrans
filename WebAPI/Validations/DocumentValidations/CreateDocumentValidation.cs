using Application.ViewModels.DocumentDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.DocumentValidations
{
    public class CreateDocumentValidation : AbstractValidator<CreateDocumentDTO>
    {
        public CreateDocumentValidation() 
        {
            RuleFor(x => x.FileType)
            .Must(value => Enum.IsDefined(typeof(FileType), value))
            .WithMessage("The value must be Hard or Soft.");
        }
    }
}
