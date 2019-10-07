using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Zip.Challenge.Common.Dto;
using Zip.Challenge.Common.Helpers;
using Zip.Challenge.Common.Queries;
using Zip.Challenge.Services.Identity.Services;

namespace Zip.Challenge.Services.Identity.Handlers
{
    public class GetUserHandler : IQueryHandler<GetUser, User>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public GetUserHandler(IUserService userService,
            ILogger<ListUsersHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<User> HandleAsync(GetUser query)
        {
            _logger.LogInformation("Getting user");

            try
            {
                var result = await _userService.GetAsync(query.Email);
                return result.ConvertTo<Domain.Models.User, User>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
