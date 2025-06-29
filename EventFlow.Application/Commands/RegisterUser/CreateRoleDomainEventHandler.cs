using Eventflow.Domain.Event.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.RegisterUser
{
    public class CreateRoleDomainEventHandler : INotificationHandler<CreateUserRoleDomainEvent>
    {
        public CreateRoleDomainEventHandler()
        {
            //Inject UserRoleRepository, RoleRepository, and any other dependencies here

        }
        public Task Handle(CreateUserRoleDomainEvent notification, CancellationToken cancellationToken)
        {
            //call the UserRoleRepository to create a new user role

            return Task.CompletedTask;  
        }
    }
}
