using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;

namespace Zip.Challenge.ApiGateway.Services
{
    public interface IUserService
    {
        Task<List<User>> ListAsync();
        Task<User> GetAsync(string email);
        Task RegisterAsync(CreateUser command);
    }
}
