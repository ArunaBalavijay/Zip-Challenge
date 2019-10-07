using System;

namespace Zip.Challenge.Common.Dto
{
    public class Account
    {
        public string UserEmail { get; set; }
        public long AccountNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
