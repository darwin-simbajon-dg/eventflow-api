﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs
{
    public record UserDto(Guid Id, string FirstName, string LastName, string Email);

}
