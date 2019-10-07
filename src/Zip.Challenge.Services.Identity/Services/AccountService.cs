using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;
using Zip.Challenge.Services.Identity.Domain.Repositories;

namespace Zip.Challenge.Services.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Account>> ListAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task CreateAsync(Account newAccount)
        {
            var account = await _repository.GetAsync(newAccount.UserEmail);
            if (account != null)
            {
                //throw new Exception("email_in_use",
                //    $"Email: '{email}' is already in use.");
            }

            await _repository.AddAsync(newAccount);
        }
    }
}
