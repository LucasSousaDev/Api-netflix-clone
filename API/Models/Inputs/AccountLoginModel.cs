using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movies_api.Domain.Entities;

namespace movies_api.API.Models.Inputs
{
    public class AccountLoginModel
    {
		public string Email { get; set; }
		public string Password { get; set; }
    }
}