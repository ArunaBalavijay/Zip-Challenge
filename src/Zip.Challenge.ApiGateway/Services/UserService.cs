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
    public class UserService : IUserService
    {
        private readonly IBusClient _busClient;
        private readonly IBusRequestClient _busRequestClient;
        private readonly ILogger<UserService> _logger;

        public UserService(IBusClient busClient, 
            IBusRequestClient busRequestClient, ILogger<UserService> logger)
        {
            _busClient = busClient;
            _busRequestClient = busRequestClient;
            _logger = logger;
        }

        public async Task<List<User>> ListAsync()
        {
            return await _busRequestClient.RequestAsync<ListUsers, List<User>>(new ListUsers());
        }

        public async Task<User> GetAsync(string email)
        {
            return await _busRequestClient.RequestAsync<GetUser, User>(new GetUser(email));
        }

        public async Task RegisterAsync(CreateUser command)
        {
            await _busClient.PublishAsync(command);
        }
    }
}
