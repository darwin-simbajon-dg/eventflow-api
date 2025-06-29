using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.DeleteEvent
{
    public record DeleteEventCommand(Guid EventId) : IRequest<Result<bool>>;
}
