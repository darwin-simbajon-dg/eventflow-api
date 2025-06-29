using Eventflow.Domain.BusinessRules;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Entities
{
    public class Profile : BaseBusinessRules
    {
        public Profile(Guid userId, int studentNumber, string firstName, string lastName, string college, Email email, Email alternativeEmail, string imageUrl)
        {
            UserId = userId;
            StudentNumber = studentNumber;
            Firstname = firstName;
            Lastname = lastName;
            College = college;
            Email = email;
            AlternativeEmail = alternativeEmail;

            Validate(new ProfileValidator(), this);
        }

        public Profile() {}

        public Guid UserId { get; set; }

        public int StudentNumber { get; private set; }

        public string Firstname { get; private set; }

        public string Lastname { get; private set; }

        public string College { get; private set; }

        public Email Email { get; private set; }

        public Email AlternativeEmail { get; private set; }

        public string? ImageUrl { get; set; }


    }
}
