using EventFlow.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Queries.Login
{
    public record LoginUserQuery(string Email, string Password)
    : IRequest<UserDto>;
}
