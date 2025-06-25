using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                //throw new InvalidEmailException(value);
                throw new Exception(value);

            Value = value.Trim().ToLowerInvariant();
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => obj is Email email && Value == email.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
