using System;

namespace Zip.Challenge.Common.Dto
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public decimal Expense { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
