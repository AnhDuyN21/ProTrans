﻿using Application.ViewModels.QuotePriceDTOs;
using FluentValidation;

namespace WebAPI.Validations.AccountValidations
{
    public class QuotePriceValidation : AbstractValidator<CUQuotePriceDTO>
    {
        public QuotePriceValidation()
        {
            RuleFor(x => x.FirstLanguageId).NotEmpty().NotNull().WithMessage("Ngôn ngữ không tồn tại trong hệ thống");
            RuleFor(x => x.SecondLanguageId).NotEmpty().NotNull().WithMessage("Ngôn ngữ không tồn tại trong hệ thống");
            RuleFor(x => x.PricePerPage).NotEmpty().NotNull().WithMessage("Đơn giá không hợp lệ");
            RuleFor(x => x.PricePerPage).GreaterThan(1000).WithMessage("Đơn giá phải lớn hơn 1,000 VND");
            RuleFor(x => x.FirstLanguageId).NotEqual(x => x.SecondLanguageId).WithMessage("Không được phép nhập hai ngôn ngữ giống nhau");
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
