using Eventflow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class ProfileModel
    {
        public Guid UserId { get; set; }
        public int StudentNumber { get; set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string College { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string AlternativeEmail { get; set; } = default!;
        public string? ImageUrl { get; set; }

        public static implicit operator ProfileModel(Profile profile)
        {
            return new ProfileModel
            {
                UserId = profile.UserId,
                StudentNumber = profile.StudentNumber,
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                College = profile.College,
                Email = profile.Email?.ToString(),
                AlternativeEmail = profile.AlternativeEmail?.ToString(),
                ImageUrl = profile.ImageUrl ?? "https://canbind.ca/wp-content/uploads/2025/01/placeholder-image-person-jpg.jpg"
            };
        }
    }
}
