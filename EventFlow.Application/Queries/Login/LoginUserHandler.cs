using EventFlow.Application.DTOs;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Queries.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        public LoginUserHandler(IUserRepository userRepository, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !user.VerifyPassword(request.Password, _hasher))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return new UserDto(user.Id, user.FirstName, user.LastName, user.Email.ToString());
        }
    }
}
