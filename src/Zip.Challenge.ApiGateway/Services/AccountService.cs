using Microsoft.Extensions.Logging;
using RawRabbit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;
using Zip.Challenge.Common.Queries;
using Zip.Challenge.Common.RabbitMq;

namespace Zip.Challenge.ApiGateway.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBusClient _busClient;
        private readonly IBusRequestClient _busRequestClient;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBusClient busClient, 
            IBusRequestClient busRequestClient, ILogger<AccountService> logger)
        {
            _busClient = busClient;
            _busRequestClient = busRequestClient;
            _logger = logger;
        }

        public async Task<List<Account>> ListAsync()
        {
            _logger.LogDebug($"Method {nameof(ListAsync)} called.");
            return await _busRequestClient.RequestAsync<ListAccounts, List<Account>>(new ListAccounts());
        }

        public async Task CreateAsync(CreateAccount command)
        {
            await _busClient.PublishAsync(command);
        }

        public bool CanCreateCustomerAccount(User user) => user.Salary - user.Expense < CreateAccount.AllowedCredit;
    }
}
