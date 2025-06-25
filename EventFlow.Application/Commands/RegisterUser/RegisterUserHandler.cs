using Eventflow.Domain.Entities;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
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
        //private readonly IPasswordHasher _hasher;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            //_hasher = hasher;
        }

        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
            {
                return Result<UserDto>.Failure("Failed to register user", "Email already exists");
            }

            //var passwordHash = _hasher.Hash(request.Password);
            var user = new User(request.FirstName, request.LastName, email);

            await _userRepository.AddAsync(user);

            var dto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email.ToString());

            return Result<UserDto>.Success(dto);
        }
    }

}
