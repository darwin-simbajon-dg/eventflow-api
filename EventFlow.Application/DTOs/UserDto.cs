using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs
{
    public record UserDto(Guid UserId, string Username, string Password);

}
