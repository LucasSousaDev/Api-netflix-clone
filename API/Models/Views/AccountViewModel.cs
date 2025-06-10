using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movies_api.Domain.Entities;

namespace movies_api.API.Models.Views
{
    public class AccountViewModel
    {
		public int Id { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }

	
		public AccountViewModel(Account account)
		{
			Id = account.Id;
			Name = account.Name;
			Email = account.Email;
		}
    }
}