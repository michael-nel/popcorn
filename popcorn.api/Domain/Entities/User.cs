using System;
using Domain.Entities.Base;
using Domain.Extensions;
using Domain.Resources;
using prmToolkit.NotificationPattern;

namespace Domain.Entities
{
    public class User : EntityBase
    {
        protected User() { }
        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

            new AddNotifications<User>(this)
                .IfNullOrInvalidLength(x => x.FirstName, 3, 150, Resource.FirstNameMustBeBetween3And150)
                .IfNullOrInvalidLength(x => x.LastName, 3, 150, Resource.LastNameMustBeBetween3And150)
                .IfNotEmail(x => x.Email, Resource.EmailIsInvalid)
                .IfNullOrInvalidLength(x => x.Password, 3, 32, Resource.PasswordMustBeBetween3and32);

            if (!string.IsNullOrEmpty(this.Password))
            {
                this.Password = Password.ConvertToMD5();
            }
            CreatedAt = DateTime.Now;
            Active = true;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
    }
}
