using Eventflow.Domain.Aggregates.UserAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.BusinessRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email.Value)
              .NotEmpty().WithMessage("Email is required");

            RuleFor(user => user.Password)
              .NotEmpty().WithMessage("Password is required");
        }
    }
}
