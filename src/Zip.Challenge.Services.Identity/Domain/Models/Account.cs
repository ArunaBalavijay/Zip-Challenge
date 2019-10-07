using System;

namespace Zip.Challenge.Services.Identity.Domain.Models
{
    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            AccountNumber = DateTime.Now.ToFileTime();
        }

        public Guid Id { get; protected set; }
        public string UserEmail { get; protected set; }
        public long AccountNumber { get; protected set; }
        public decimal CreditLimit { get; protected set; }
        public decimal Balance { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
    }
}
