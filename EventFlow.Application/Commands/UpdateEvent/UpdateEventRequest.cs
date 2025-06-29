using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.UpdateEvent
{
    public record UpdateEventRequest(
        string EventId,
        string Title,
        string Date,
        string Time,
        string Headline,
        string Venue,
        string Description,
        string Notes,
        string ImageUrl,
        string Link);
}
