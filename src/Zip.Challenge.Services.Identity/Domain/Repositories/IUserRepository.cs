using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;

namespace Zip.Challenge.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<List<User>> GetAsync();
        Task AddAsync(User user);
    }
}