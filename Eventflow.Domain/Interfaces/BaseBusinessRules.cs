using Eventflow.Domain.BusinessRules;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Interfaces
{
    public abstract class BaseBusinessRules
    {
        public bool IsValid { get { return !Errors.Any(); } }

        public List<string> Errors { get; set; } = new List<string>();

        protected void Validate<T>(AbstractValidator<T> validator, T instance)
        {
            var validationResult = validator.Validate(instance);

            if (!validationResult.IsValid)
            {
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
        }
    }
}
