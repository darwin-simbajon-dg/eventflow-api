using Eventflow.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs
{
    public class EventDTO
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Venue { get; set; }
        public string ImageUrl { get; set; }
        public bool Attended { get; set; } = false; // Default to false, indicating not attended
        public bool IsRegistered { get; set; } = false; // Default to false, indicating not registered
        //public string Timeline { get; set; }

        public static implicit operator EventDTO(EventModel model)
        {
            return new EventDTO
            {
                EventId = model.EventId,
                Title = model.EventName,
                Description = model.EventDetails,
                ImageUrl = model.ImageUrl,
                Headline = model.EventHeadline,
                Venue = model.Location,
                Notes = model.Notes,
                Date = model.EventSchedule.ToString("yyyy-MM-dd"), // Format date as needed
                Time = model.EventSchedule.ToString("HH:mm"), // Format time as needed              
                //IsRegistered = false, // Default to false, indicating not registered
                //Attended = false, // Default to false, indicating not attended
                //Timeline = GetTimeline(model.DateCreated)
            };
        }

        //private static string GetTimeline(DateTime dateCreated)
        //{
        //    //Get the time lapse if dateCreate is today set to "Today" then x days ago if not today
        //    var timeSpan = DateTime.Now - dateCreated;
        //    if (timeSpan.TotalDays < 1)
        //    {
        //        return "Today";
        //    }
        //    else if (timeSpan.TotalDays < 7)
        //    {
        //        return $"{(int)timeSpan.TotalDays} days ago";
        //    }
        //    else if (timeSpan.TotalDays < 30)
        //    {
        //        return $"{(int)(timeSpan.TotalDays / 7)} weeks ago";
        //    }
        //    else
        //    {
        //        return $"{(int)(timeSpan.TotalDays / 30)} months ago";
        //    }
        //}

        public static implicit operator EventDTO(EventRegistration registration)
        {
            return new EventDTO
            {
                EventId = registration.EventId,
                Title = registration.EventName,
                Description = registration.EventDetails,
                IsRegistered = registration.IsRegistered,
                //Timeline = GetTimeline(registration.DateCreated),

            };
        }
    }
}