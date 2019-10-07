using System;

namespace Zip.Challenge.Services.Identity.Domain.Models
{
    public class User
    {
        public User()
        {
            //if (string.IsNullOrWhiteSpace(email))
            //{
            //    throw new Exception("empty_user_email",
            //        "User email can not be empty.");
            //}

            //if (string.IsNullOrWhiteSpace(name))
            //{
            //    throw new Exception("empty_user_name",
            //        "User name can not be empty.");
            //}

            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public decimal Salary { get; protected set; }
        public decimal Expense { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
    }
}
