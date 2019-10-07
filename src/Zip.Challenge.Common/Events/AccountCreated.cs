using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.Challenge.Common.Events
{
    public class AccountCreated : IEvent
    {
        protected AccountCreated()
        {
        }

        public AccountCreated(string email, decimal creditLimit, decimal balance)
        {
            UserEmail = email;
            CreditLimit = creditLimit;
            Balance = Balance;
        }

        public string UserEmail { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
    }
}
