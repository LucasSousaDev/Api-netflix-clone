using movies_api.Domain.Entities;

namespace movies_api.API.Models.Inputs
{
    public class AccountUpdateModel
    {
		public int Id { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public void ApplyToEntity(Account account)
		{
			account.Name = Name ?? account.Name;
			account.Email = Email ?? account.Email;
			account.Password = Password.Length > 0
				? BCrypt.Net.BCrypt.HashPassword(Password)
				: account.Password;
		}
    }
}