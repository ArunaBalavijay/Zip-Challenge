﻿using Microsoft.Extensions.Logging;
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
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService,
            ILogger<CreateUserHandler> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with name: '{command.LastName}, {command.FirstName}'.");

            try
            {
                await _userService.RegisterAsync(command.ConvertTo<CreateUser, User>());
                await _busClient.PublishAsync(new UserCreated(command.Email, command.FirstName, command.LastName));

                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.LastName}, {command.FirstName}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, "error"));
            }
        }
    }
}
