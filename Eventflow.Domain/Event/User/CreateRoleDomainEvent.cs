using Eventflow.Domain.Entities;
using Eventflow.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Event.User
{
    public class CreateUserRoleDomainEvent : IDomainEvent, INotification
    {
        public DateTime OccurredOn => throw new NotImplementedException();

        public Guid UserId { get; set; }
        public Role Role { get; set; }

        public CreateUserRoleDomainEvent(Guid userId, Role role)
        {
            Role = role;
        }
    }
}
