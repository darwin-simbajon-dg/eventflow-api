using Eventflow.Infrastructure.Data.Models;
using Eventflow.Shared;
using EventFlow.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Queries.GetEvents
{
    public record GetEventsQuery(Guid UserId) : IRequest<Result<IEnumerable<EventDTO>>>;
}

