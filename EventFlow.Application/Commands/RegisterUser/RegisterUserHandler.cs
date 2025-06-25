using Eventflow.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
                throw new Exception("User already exists");

            var passwordHash = _hasher.Hash(request.Password);
            var user = new User(request.FirstName, request.LastName, email, passwordHash);

            await _userRepository.AddAsync(user);

            return new UserDto(user.Id, user.FirstName, user.LastName, user.Email.ToString());
        }
    }
}
