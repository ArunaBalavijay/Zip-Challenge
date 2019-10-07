using System.ComponentModel.DataAnnotations;

namespace Zip.Challenge.Common.Commands
{
    public class CreateUser : ICommand
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Range(0, double.PositiveInfinity)]
        public decimal Salary { get; set; }
        [Range(0, double.PositiveInfinity)]
        public decimal Expense { get; set; }
    }
}
