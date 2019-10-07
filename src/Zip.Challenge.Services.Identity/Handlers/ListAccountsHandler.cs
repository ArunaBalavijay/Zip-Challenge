using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Common.Dto;
using Zip.Challenge.Common.Helpers;
using Zip.Challenge.Common.Queries;
using Zip.Challenge.Services.Identity.Services;

namespace Zip.Challenge.Services.Identity.Handlers
{
    public class ListAccountsHandler : IQueryHandler<ListAccounts, List<Account>>
    {
        private readonly ILogger _logger;
        private readonly IAccountService _accountService;

        public ListAccountsHandler(IAccountService accountService,
            ILogger<ListAccountsHandler> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<List<Account>> HandleAsync(ListAccounts query)
        {
            _logger.LogInformation("Listing accounts");

            try
            {
                var result = await _accountService.ListAsync();
                return result.ConvertListTo<Domain.Models.Account, Account>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
