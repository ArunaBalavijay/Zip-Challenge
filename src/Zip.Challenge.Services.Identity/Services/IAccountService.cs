using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;

namespace Zip.Challenge.Services.Identity.Services
{
    public interface IAccountService
    {
        Task<List<Account>> ListAsync();
        Task CreateAsync(Account newUser);
    }
}
