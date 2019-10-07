using System.ComponentModel.DataAnnotations;

namespace Zip.Challenge.Common.Commands
{
    public class CreateAccount : ICommand
    {
        public const decimal AllowedCredit = 1000;

        [Required, EmailAddress]
        public string UserEmail { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
    }
}
