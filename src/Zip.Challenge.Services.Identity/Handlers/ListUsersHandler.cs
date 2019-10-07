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
    public class ListUsersHandler : IQueryHandler<ListUsers, List<User>>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public ListUsersHandler(IUserService userService,
            ILogger<ListUsersHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<List<User>> HandleAsync(ListUsers query)
        {
            _logger.LogInformation("Listing users");

            try
            {
                var result = await _userService.ListAsync();
                return result.ConvertListTo<Domain.Models.User, User>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
