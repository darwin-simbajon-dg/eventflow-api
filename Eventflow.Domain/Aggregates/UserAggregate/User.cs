using Eventflow.Domain.BusinessRules;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using EventFlow.Domain.Event.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Aggregates.UserAggregate
{
    public class User : BaseBusinessRules
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Guid UserId { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }
        private User() { } // For EF Core or Dapper

        public User(Email email, string password)
        {
            UserId = Guid.NewGuid();
            Email = email;
            Password = password;

            Validate(new UserValidator(), this);      
        }

        public User(int studentNumber, string firstname, string lastname, string college, Email email, Email alternativeEmail, string password)
        {
            UserId = Guid.NewGuid();
            Email = email;
            Password = password;

            Validate(new UserValidator(), this);

            AddDomainEvent(new UserRegisteredDomainEvent(UserId, studentNumber, Email.ToString(), firstname, lastname, college, alternativeEmail.ToString()));
        }

        public bool VerifyPassword(string password)
        {
            return Password == password;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

    }

}
