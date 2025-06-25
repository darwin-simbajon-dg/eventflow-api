using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Shared
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public List<string> Errors { get; } = new();

        private Result(bool isSuccess, T? value, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            if (errors is not null)
                Errors = errors;
        }

        public static Result<T> Success(T value) => new(true, value, null);

        public static Result<T> Failure(params string[] errors) =>
            new(false, default, errors.ToList());

        public static Result<T> Failure(List<string> errors) =>
            new(false, default, errors);
    }

}
