using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using movies_api.Domain.Entities;
using movies_api.Domain.Interfaces;
using movies_api.Infrastructure.Persistence;

namespace Accounts_api.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;

		public AccountRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Account?> GetAccountByIdAsync(int id)
		{
			return await _appDbContext.Accounts
				.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<Account?> GetAccountByEmailAsync(string email)
		{
			return await _appDbContext.Accounts
				.FirstOrDefaultAsync(a => a.Email == email);
		}

		public async Task<bool> AccountExistsAsync(string email)
		{
			Account? account = await _appDbContext.Accounts
				.FirstOrDefaultAsync(a => a.Email == email);
			
			return account != null;
		}

		public async Task<Account?> CreateAccountAsync(Account newAccount)
		{
			EntityEntry<Account> accountCreated = await _appDbContext.Accounts.AddAsync(newAccount);

			return (await CommitAsync())
				? accountCreated.Entity
				: null;
		}

		public async Task<Account?> UpdateAccountAsync(Account accountUpdated)
		{
			var trackedEntity = _appDbContext.Accounts.Update(accountUpdated);

			return await CommitAsync()
				? trackedEntity.Entity
				: null;
		}

		public async Task<bool> RemoveAccountAsync(Account account)
		{
			_appDbContext.Remove(account);

			return (await CommitAsync());
		}

		public async Task<bool> CommitAsync()
		{
			int saveResult = await _appDbContext.SaveChangesAsync();
			
			return (saveResult > 0);
		}
	}
}
