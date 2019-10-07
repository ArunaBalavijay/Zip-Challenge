using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;

namespace Zip.Challenge.Services.Identity.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAsync(string email);
        Task<List<Account>> GetAsync();
        Task AddAsync(Account account);
    }
}