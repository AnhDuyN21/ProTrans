using Application.ViewModels.AssignmentNotarizationDTOs;
using Application.ViewModels.QuotePriceDTOs;
using Domain.Enums;
using FluentValidation;

namespace WebAPI.Validations.AccountValidations
{
    public class AssigmentNotarizationValidation : AbstractValidator<CUAssignmentNotarizationDTO>
    {
        public AssigmentNotarizationValidation()
        {
          RuleFor(x => x.NumberOfNotarization).NotEmpty().NotNull().GreaterThan(0).WithMessage("If customer need notarization, this field has to be greater than 0, you idiot. Also not null or empty");
          
        }
    }
}
