using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using movies_api.API.Models.Inputs;
using movies_api.API.Models.Views;
using movies_api.Domain.Entities;
using movies_api.Domain.Interfaces;

namespace movies_api.Application.Services
{
	public class AccountService : IAccountService
	{
		 private readonly IConfiguration _configuration;
		private readonly IAccountRepository _accountRepository;

        public AccountService(IConfiguration configuration, IAccountRepository accountRepository)
		{
			_configuration = configuration;
			_accountRepository = accountRepository;
		}

		public async Task<AccountViewModel> GetAccountByIdAsync(int id)
		{
			Account? account = await _accountRepository.GetAccountByIdAsync(id)
				?? throw new KeyNotFoundException($"Conta com ID {id} não encontrado.");

			return new AccountViewModel(account);
		}

		public async Task<string> LoginAsync(AccountLoginModel accountLogin)
		{
			string email = accountLogin.Email;

			Account? account = await _accountRepository.GetAccountByEmailAsync(email)
				?? throw new KeyNotFoundException($"Conta com e-mail {email} não encontrado.");
			
			if (!BCrypt.Net.BCrypt.Verify(accountLogin.Password, account.Password))
				throw new UnauthorizedAccessException("E-mail ou Senha inválido!");
			
			return GenerateToken(email);
		}

		public async Task<AccountViewModel> CreateAccountAsync(AccountInputModel accountInput)
		{
			if (await _accountRepository.AccountExistsAsync(accountInput.Email))
				throw new InvalidOperationException($"Já existe uma conta com o e-mail '{accountInput.Email}'.");
			
			Account newAccount = accountInput.ToEntity();
			newAccount.Password = BCrypt.Net.BCrypt.HashPassword(accountInput.Password);

			Account? accountCreated = await _accountRepository.CreateAccountAsync(newAccount)
				?? throw new Exception($"Não foi possível criar a conta de e-mail {newAccount.Email}");
			
			return new AccountViewModel(accountCreated);
		}

		public async Task<AccountViewModel> UpdateAccountAsync(AccountUpdateModel accountUpdated)
		{
			Account foundAccount = await _accountRepository.GetAccountByIdAsync(accountUpdated.Id)
				?? throw new KeyNotFoundException($"Conta com ID {accountUpdated.Id} não encontrado.");
			
			accountUpdated.ApplyToEntity(foundAccount);

			return (await _accountRepository.CommitAsync())
				? new AccountViewModel(foundAccount)
				: throw new Exception($"Não foi possível atualizar o usuário do e-mail: {foundAccount.Email}.");
		}

		public async Task<bool> RemoveAccountAsync(int id)
		{
			Account? foundAccount = await _accountRepository.GetAccountByIdAsync(id)
				?? throw new KeyNotFoundException($"Conta com ID {id} não encontrado.");

			return await _accountRepository.RemoveAccountAsync(foundAccount);
		}

		public string GenerateToken(string email)
		{
			IConfigurationSection? jwtSettings = _configuration.GetSection("jwt");
			string? secretKey = jwtSettings.GetValue<string>("key");
			string? issuer = jwtSettings.GetValue<string>("issuer");
			string? audience = jwtSettings.GetValue<string>("audience");

			SymmetricSecurityKey key = new (Convert.FromBase64String(secretKey!));
			SigningCredentials creds = new (key, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new (
				issuer: issuer,
				audience: audience,
				claims: [new (ClaimTypes.Email, email)],
				expires: DateTime.UtcNow.AddHours(6),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
