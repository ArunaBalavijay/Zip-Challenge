using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;

namespace Zip.Challenge.Services.Identity.Services
{
    public interface IUserService
    {
        Task<List<User>> ListAsync();
        Task<User> GetAsync(string email);
        Task RegisterAsync(User newUser);
    }
}
