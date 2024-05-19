using FluentValidation;
using PROG_Slutprojekt_frontend.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.Validators
{
    internal class SignInSchema : AbstractValidator<SignInUser>
    {
        public SignInSchema()
        {
            RuleFor(u => u.email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email address");
            RuleFor(u => u.password).NotEmpty().WithMessage("Password is required").Length(8, 255).WithMessage("Password must be between 8-255 characters");

        }
    }
}
