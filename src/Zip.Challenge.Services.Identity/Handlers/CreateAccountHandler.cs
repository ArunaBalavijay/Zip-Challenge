using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Events;
using Zip.Challenge.Common.Helpers;
using Zip.Challenge.Services.Identity.Domain.Models;
using Zip.Challenge.Services.Identity.Services;

namespace Zip.Challenge.Services.Identity.Handlers
{
    public class CreateAccountHandler : ICommandHandler<CreateAccount>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IAccountService _accountService;

        public CreateAccountHandler(IBusClient busClient,
            IAccountService accountService,
            ILogger<CreateAccountHandler> logger)
        {
            _busClient = busClient;
            _accountService = accountService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateAccount command)
        {
            _logger.LogInformation($"Creating account for: '{command.UserEmail}'.");

            try
            {
                await _accountService.CreateAsync(command.ConvertTo<CreateAccount, Account>());
                await _busClient.PublishAsync(new AccountCreated(command.UserEmail, command.CreditLimit, command.Balance));

                _logger.LogInformation($"Account: '{command.UserEmail}' was created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateAccountRejected(command.UserEmail,
                    ex.Message, "error"));
            }
        }
    }
}
