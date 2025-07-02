using Eventflow.Infrastructure.Interfaces;
using Eventflow.Infrastructure.Repositories;
using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.ConfirmAttendance
{
    public class ConfirmAttendanceHandler : IRequestHandler<ConfirmAttendanceCommand, Result<bool>>
    {
        private readonly IEventAttendanceRepository _eventAttendanceRepository;

        public ConfirmAttendanceHandler(IEventAttendanceRepository eventAttendanceRepository)
        {
            _eventAttendanceRepository = eventAttendanceRepository;
        }

        public async Task<Result<bool>> Handle(ConfirmAttendanceCommand request, CancellationToken cancellationToken)
        {
            var isSuccesfull = await _eventAttendanceRepository.ConfirmAttendance(request.request.UserId, request.request.EventId);

            if (!isSuccesfull)
            {
                return Result<bool>.Failure("Failed to confirm attendance");
            }

            return Result<bool>.Success(true);

        }
    }
}
