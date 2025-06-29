using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.UpdateProfile
{
   public record UpdateProfileCommand(
        Guid UserId,
        string FullName,
        int StudentNumber,
        string College,
        string Email,
        string AlternateEmail,
        string ImageUrl) : IRequest<Result<bool>>;
}
