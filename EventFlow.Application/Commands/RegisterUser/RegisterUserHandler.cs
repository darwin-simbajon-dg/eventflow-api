using Eventflow.Domain.Aggregates.UserAggregate;
using Eventflow.Domain.Entities;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using EventFlow.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        //private readonly IPasswordHasher _hasher;

        public RegisterUserHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
            {
                return Result<UserDto>.Failure("Failed to register user", "Email already exists");
            }

            var user = new User(
                request.StudentNumber,
                request.FirstName, 
                request.LastName, 
                request.College, email, 
                new Email(request.AlternativeEmail), 
                request.Password
                );

            if (!user.IsValid)
            {
                return Result<UserDto>.Failure("Failed to register user", string.Join(", ", user.Errors));
            }

            var isSuccessful = await _userRepository.AddAsync(user);

            if (!isSuccessful)
            {
                return Result<UserDto>.Failure("Failed to register user", "An error occurred while saving the user.");
            }

            foreach (var domainEvent in user.DomainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            var dto = new UserDto(user.UserId, user.Email.ToString(), user.Password);

            return Result<UserDto>.Success(dto);
        }
    }

}
