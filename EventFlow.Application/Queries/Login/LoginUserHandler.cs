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
using Eventflow.Infrastructure.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace EventFlow.Application.Queries.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public LoginUserHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var email = new Email(request.Username);

                var profile = await _userRepository.VerifyUser(email, request.Password);
                //if (user == null || !user.VerifyPassword(request.Password))
                //    throw new UnauthorizedAccessException("Invalid email or password.");

                var userRole = await _userRoleRepository.GetUserRoleById(profile.UserId);

                var dto = new ProfileDTO(
                    profile.UserId,
                    profile.StudentNumber,
                    profile.Firstname,
                    profile.Lastname,
                    profile.Email.ToString(),
                    profile.Email.ToString(),
                    profile.College,
                    userRole.RoleName,
                    profile.ImageUrl ?? string.Empty
                   );

                var token = GenerateJwtToken(dto);

                return Result<string>.Success(token);
            }
            catch (Exception)
            {

                return Result<string>.Failure("Authentication error.");
            }
         
        }

        private string GenerateJwtToken(ProfileDTO profile)
        {
            var claims = new[]
            {
            //new Claim(ClaimTypes.Name, profile.Email),
            //new Claim(ClaimTypes.NameIdentifier, profile.UserId.ToString()),
            //new Claim(ClaimTypes.Role,profile.Role),
            new Claim("Role", profile.Role),
            new Claim("UserId", profile.UserId.ToString()),
            new Claim("Email", profile.Email),
            new Claim("AlternativeEmail", profile.AlternativeEmail),
            new Claim("Fullname", profile.FirstName + ' ' + profile.LastName),
            new Claim("StudentNumber", profile.StudentNumber.ToString()),
            new Claim("College", profile.College),
            new Claim("ImageUrl", string.IsNullOrEmpty(profile.ImageUrl) 
            ? "https://canbind.ca/wp-content/uploads/2025/01/placeholder-image-person-jpg.jpg" 
            : profile.ImageUrl),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecureAndLongEnoughSecretKey123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "eventflow-api",
                audience: "eventflow-users",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
