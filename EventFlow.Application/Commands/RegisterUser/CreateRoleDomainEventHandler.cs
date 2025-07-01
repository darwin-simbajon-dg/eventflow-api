using Eventflow.Domain.Event.User;
using Eventflow.Infrastructure.Interfaces;
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
        private readonly IUserRoleRepository _userRoleRepository;

        public CreateRoleDomainEventHandler(IUserRoleRepository userRoleRepository)
        {
            //Inject UserRoleRepository, RoleRepository, and any other dependencies here

            _userRoleRepository = userRoleRepository;
        }
        public async Task Handle(CreateUserRoleDomainEvent notification, CancellationToken cancellationToken)
        {
            //call the UserRoleRepository to create a new user role
            await _userRoleRepository.CreateRoleforUserId(notification.UserId);
        }
    }
}
