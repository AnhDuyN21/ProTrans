using Application.ViewModels.AccountDTOs;
using FluentValidation;

namespace WebAPI.Validations.AccountValidations
{
    public class RegisterDTOValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Must(email => email.EndsWith("@gmail.com"))
                .WithMessage("Email must end with @gmail.com");
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^0[0-9]{9}$")
                .WithMessage("The phone number must have 10 digits and start with 0");
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long");
        }
    }
}
