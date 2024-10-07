
using Application.ViewModels.AttachmentDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.AttachmentValidations
{
    public class CreateAttachmentDTOValidation : AbstractValidator<CreateAttachmentDTO>
    {
        public CreateAttachmentDTOValidation()
        {
            RuleFor(x => x.FileType)
            .Must(value => Enum.IsDefined(typeof(FileType), value))
            .WithMessage("The value must be Hard or Soft.");
        }
    }
}
