using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_api.API.Models.Inputs;
using movies_api.API.Models.Views;
using movies_api.Domain.Interfaces;

namespace movies_api.API.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}
		
		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAccountByIdAsync([FromRoute] int id)
		{
			AccountViewModel account = await _accountService.GetAccountByIdAsync(id);
			return Ok(account);
		}
	
		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync(AccountLoginModel accountLogin)
		{
			string? token = await _accountService.LoginAsync(accountLogin);
			return Ok(token);
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAccountAsync([FromBody] AccountInputModel accountInput)
		{
			AccountViewModel account = await _accountService.CreateAccountAsync(accountInput);
			return Created($"/Account/{account.Id}", account);
		}

		[Authorize]
		[HttpPut]
		public async Task<IActionResult> UpdateAccountAsync([FromBody] AccountUpdateModel accountUpdate)
		{
			AccountViewModel account = await _accountService.UpdateAccountAsync(accountUpdate);
			return Ok(account);
		}

		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveAccountAsync([FromRoute] int id)
		{
			await _accountService.RemoveAccountAsync(id);
			return Ok();
		}
    }
}