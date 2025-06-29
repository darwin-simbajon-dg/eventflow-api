using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.ConfirmAttendance
{
    public record ConfirmAttendanceRequest(string EventId, string UserId);
}
