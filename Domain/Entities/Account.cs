using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movies_api.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public Account() {}
    }
}
