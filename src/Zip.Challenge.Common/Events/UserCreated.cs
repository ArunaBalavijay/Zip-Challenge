using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.Challenge.Common.Events
{
    public class UserCreated : IEvent
    {
        protected UserCreated()
        {
        }

        public UserCreated(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
