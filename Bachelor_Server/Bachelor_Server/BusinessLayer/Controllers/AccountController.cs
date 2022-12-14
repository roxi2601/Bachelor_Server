using Bachelor_Server.BusinessLayer.Services.Account;
using Bachelor_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("getAccount")]
    public async Task<Account> GetLoggedAccount()
    {
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        return await
            _accountService.GetLoggedAccount(JsonConvert.DeserializeObject<Account>(body.Result));
    }

    [HttpPost("createAccount")]
    public async Task<string> CreateAccount()
    {
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        return await
            _accountService.CreateAccount(JsonConvert.DeserializeObject<Account>(body.Result));
    }
    
    [HttpGet("accounts")]
    public async Task<List<Account>> GetAccounts()
    {
        return await _accountService.GetAccounts();
    }
    
    [HttpPatch("editAccount")]
    public async Task EditAccount()
    {
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        await _accountService.EditAccount(
            JsonConvert.DeserializeObject<Account>(body.Result));
    }

    [HttpDelete("deleteAccount/{id}")]
    public async Task DeleteAccount(int id)
    {
        await _accountService.DeleteAccount(id);
    }
}