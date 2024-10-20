using Application.ViewModels.AssignmentTranslationDTOs;
using FluentValidation;

namespace WebAPI.Validations.AccountValidations
{
    public class AssigmentTranslationValidation : AbstractValidator<CUAssignmentTranslationDTO>
    {
        public AssigmentTranslationValidation()
        {
            RuleFor(x => x.Deadline).NotEmpty().WithMessage("Date is required.")
            .Must(date => date > DateTime.Now).WithMessage("Date must be in the future.");

            //RuleFor(x => x.FullName).NotEmpty().MinimumLength(5);
            //RuleFor(x => x.Email).NotEmpty().EmailAddress().Must(email => email.EndsWith("@gmail.com"))
            //    .WithMessage("Email must end with @gmail.com");
            //RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^0[0-9]{9}$")
            //    .WithMessage("The phone number must have 10 digits and start with 0");
            //RuleFor(x => x.Address).NotEmpty();
            //RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            //    .WithMessage("Password must be at least 8 characters long");
            //RuleFor(x => x.Gender)
            //    .Must(value => Enum.IsDefined(typeof(Gender), value))
            //    .WithMessage("The value must be Male, Female or Others.");
        }
    }
}
