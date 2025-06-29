using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.UpdateProfile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, Result<bool>>
    {
        private readonly IProfileRepository _profileRepository;

        public UpdateProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Result<bool>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = new ProfileModel()
            {
                UserId = request.UserId,
                Firstname = request.FullName.Split(' ')[0],
                Lastname = request.FullName.Split(' ').Length > 1 ? string.Join(' ', request.FullName.Split(' ').Skip(1)) : string.Empty,
                StudentNumber = request.StudentNumber,
                College = request.College,
                Email = request.Email,
                AlternativeEmail = request.AlternateEmail,
                ImageUrl = request.ImageUrl
            };

            var existingProfile = await _profileRepository.GetProfileByUserId(request.UserId);

            if (existingProfile == null)
            {
                return Result<bool>.Failure("Profile not found.");
            }

            existingProfile.Firstname = profile.Firstname;
            existingProfile.Lastname = profile.Lastname;
            existingProfile.StudentNumber = profile.StudentNumber;
            existingProfile.College = profile.College;
            existingProfile.Email = profile.Email;
            existingProfile.AlternativeEmail = profile.AlternativeEmail;
            existingProfile.ImageUrl = profile.ImageUrl;

            var isUpdated = await _profileRepository.UpdateProfile(existingProfile);

            if (!isUpdated)
            {
                return Result<bool>.Failure("Failed to update profile.");
            }

            return Result<bool>.Success(true);
        }
    }
}
