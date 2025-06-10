using movies_api.API.Models.Inputs;
using movies_api.API.Models.Views;

namespace movies_api.Domain.Interfaces
{
    public interface IAccountService
    {
		Task<string> LoginAsync(AccountLoginModel accountLogin);
		Task<AccountViewModel> GetAccountByIdAsync(int id);
		Task<AccountViewModel> CreateAccountAsync(AccountInputModel accountInput);
		Task<AccountViewModel> UpdateAccountAsync(AccountUpdateModel accountUpdate);
		Task<bool> RemoveAccountAsync(int id);
		string GenerateToken(string email);
    }
}
