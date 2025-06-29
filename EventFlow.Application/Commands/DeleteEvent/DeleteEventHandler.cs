using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.DeleteEvent
{
   public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, Result<bool>>
    {
        private readonly IEventRepository _eventRepository;
        public DeleteEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<Result<bool>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _eventRepository.DeleteEvent(request.EventId);
            if (!isDeleted)
            {
                return Result<bool>.Failure("Failed to delete event.");
            }
            return Result<bool>.Success(true);
        }
    }
}
