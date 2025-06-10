using movies_api.Domain.Entities;

namespace movies_api.API.Models.Inputs
{
    public class AccountInputModel
    {
        public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public Account ToEntity()
		{
			return new Account()
			{
				Name = Name,
				Email = Email,
				Password = Password
			};
		}
    }
}