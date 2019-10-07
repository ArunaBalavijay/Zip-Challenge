using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;
using Zip.Challenge.Services.Identity.Domain.Repositories;

namespace Zip.Challenge.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> ListAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<User> GetAsync(string email)
        {
            return await _repository.GetAsync(email);
        }

        public async Task RegisterAsync(User newUser)
        {
            var user = await _repository.GetAsync(newUser.Email);
            if (user != null)
            {
                throw new ValidationException($"Email: '{newUser.Email}' is already in use.");
            }
            
            await _repository.AddAsync(newUser);
        }
    }
}
