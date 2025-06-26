using Eventflow.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Event.User
{
    public class UserRegisteredDomainEvent : IDomainEvent, INotification
    {
        public Guid UserId { get; }
        public int StudentNumber { get; set; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string College { get; }
        public string AlternativeEmail { get; }

        public DateTime OccurredOn { get; }

        public UserRegisteredDomainEvent(Guid userId, int studentNumber, string email, string firstName, string lastName, string college, string alternativeEmail)
        {
            UserId = userId;
            StudentNumber = studentNumber;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            College = college;
            AlternativeEmail = alternativeEmail;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
