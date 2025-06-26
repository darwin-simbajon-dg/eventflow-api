using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs
{
    public record ProfileDTO(Guid UserId, int StudentNumber, string FirstName, string LastName, string Email, string AlternativeEmail, string College);
}
