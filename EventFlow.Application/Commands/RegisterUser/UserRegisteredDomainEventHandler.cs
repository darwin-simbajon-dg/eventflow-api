using Eventflow.Domain.Entities;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using Eventflow.Infrastructure.Interfaces;
using EventFlow.Domain.Event.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.RegisterUser
{
    public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IMediator _mediator;
        private readonly IProfileRepository _profileRepository;

        public UserRegisteredDomainEventHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var profile = new Profile(
                notification.UserId,
                notification.StudentNumber,
                notification.FirstName,
                notification.LastName,
                notification.College,
                new Email(notification.Email),
                new Email(notification.AlternativeEmail)
                );

            var foundProfile = await _profileRepository.GetProfileByUserId(notification.UserId);

            if (foundProfile != null)
            {
                //send error message to SignalR for duplicate profile
                return;
            }
            // Add the profile to the repository

            var isSuccessful = await _profileRepository.AddProfile(profile);

            if (!isSuccessful)
            {
                //send error message to SignalR for failed transaction
            }


        }
    }
}
