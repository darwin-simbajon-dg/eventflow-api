using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.UpdateProfile
{
    public record UpdateProfileRequest(
        Guid UserId,
        string FullName,
        int StudentNumber,
        string College,
        string Email,
        string AlternateEmail,
        string ImageBase64);
}
