using Eventflow.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.BusinessRules
{
    public class ProfileValidator : AbstractValidator<Profile>
    {
        public ProfileValidator()
        {
            RuleFor(profile => profile.StudentNumber)
                .GreaterThan(0).WithMessage("Student number must be greater than 0.");

            RuleFor(profile => profile.Firstname)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(profile => profile.Lastname)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(profile => profile.College)
                .NotEmpty().WithMessage("College is required.")
                .MaximumLength(100).WithMessage("College must not exceed 100 characters.");

            //RuleFor(profile => profile.Email)
            //    .NotEmpty().WithMessage("Email is required.")
            //    .EmailAddress().WithMessage("Invalid email format.");

            //RuleFor(profile => profile.AlternativeEmail)
            //    .EmailAddress().WithMessage("Invalid alternative email format.")
            //    .When(profile => !string.IsNullOrEmpty(profile.AlternativeEmail));
        }
    }
}
