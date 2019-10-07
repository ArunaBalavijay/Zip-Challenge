using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;

namespace Zip.Challenge.ApiGateway.Services
{
    public interface IAccountService
    {
        Task<List<Account>> ListAsync();
        Task CreateAsync(CreateAccount command);
        bool CanCreateCustomerAccount(User user);
    }
}
