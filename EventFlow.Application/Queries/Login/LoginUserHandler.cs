using EventFlow.Application.DTOs;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventflow.Shared;

namespace EventFlow.Application.Queries.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, Result<ProfileDTO>>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<ProfileDTO>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            var profile = await _userRepository.VerifyUser(email, request.Password);
            //if (user == null || !user.VerifyPassword(request.Password))
            //    throw new UnauthorizedAccessException("Invalid email or password.");

            var dto = new ProfileDTO(
                profile.UserId, 
                profile.StudentNumber, 
                profile.Firstname, 
                profile.Lastname, 
                profile.Email.ToString(), 
                profile.Email.ToString(), 
                profile.College
               );

            return Result<ProfileDTO>.Success(dto);
        }
    }
}
