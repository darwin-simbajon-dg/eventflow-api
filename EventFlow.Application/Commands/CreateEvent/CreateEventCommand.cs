﻿using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.CreateEvent
{
   public record CreateEventCommand(CreateEventRequest CreateEventRequest) : IRequest<Result<bool>>;
}
