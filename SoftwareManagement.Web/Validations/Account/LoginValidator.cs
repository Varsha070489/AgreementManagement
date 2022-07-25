using FluentValidation;
using SoftwareManagement.Application.DTOs.Request.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Validations.Account
{
    public class LoginValidator : AbstractValidator<AuthenticateRequest>
    {
        public LoginValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Email id field should not empty.")
                .EmailAddress().WithMessage("Please enter valid email id.");
            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("Password field should not empty.");
        }
    }
}
