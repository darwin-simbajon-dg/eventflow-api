using Eventflow.Shared;
using EventFlow.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.RegisterUser
{

    public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password)
        : IRequest<Result<UserDto>>;

}
