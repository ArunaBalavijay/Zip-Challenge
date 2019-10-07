using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Challenge.Services.Identity.Domain.Models;

namespace Zip.Challenge.Services.Identity.Domain.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoDatabase _database;
        public AccountRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Account> GetAsync(string email)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.UserEmail == email);

        public async Task<List<Account>> GetAsync()
            => await Collection
                .AsQueryable()
                .ToListAsync();

        public async Task AddAsync(Account account)
            => await Collection.InsertOneAsync(account);

        private IMongoCollection<Account> Collection
            => _database.GetCollection<Account>("Accounts");
    }
}
