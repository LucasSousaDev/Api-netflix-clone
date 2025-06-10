using movies_api.Domain.Entities;

namespace movies_api.Domain.Interfaces
{
    public interface IAccountRepository
    {
		Task<Account?> GetAccountByIdAsync(int id);
		Task<Account?> GetAccountByEmailAsync(string email);
		Task<Account?> CreateAccountAsync(Account newAccount);
		Task<Account?> UpdateAccountAsync(Account accountUpdated);
		Task<bool> RemoveAccountAsync(Account account);
		Task<bool> CommitAsync();
    }
}